using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using VendorService.API.Middlewares;
using VendorService.Application.Configuration;
using VendorService.Application.DTOs;
using VendorService.Application.Features.Commands;
using VendorService.Application.Features.Queries;
using VendorService.Application.Mappings;
using VendorService.Domain.Repositories;
using VendorService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vendor Service API", Version = "v1" });
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));
builder.Services.AddScoped<IVendorRepository, VendorRepository>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);
builder.Services.AddAutoMapper(typeof(VendorMappingProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<CorrelationIdMiddleware>();

app.MapGet("/vendors", async (IMediator mediator, HttpContext httpContext, ILogger<Program> logger) =>
{
    var correlationId = httpContext.Request.Headers["X-Correlation-Id"].ToString();
    logger.LogInformation("Processing GetAllVendors request - CorrelationId: {CorrelationId}", correlationId);

    var vendors = await mediator.Send(new GetAllVendorsQuery());
    return Results.Ok(vendors);
});

app.MapGet("/vendors/{id:guid}", async (IMediator mediator, Guid id, ILogger<Program> logger) =>
{
    logger.LogInformation("Fetching vendor with ID {Id}", id);
    var vendor = await mediator.Send(new GetVendorByIdQuery(id));
    if (vendor is null)
    {
        logger.LogWarning("Vendor with ID {Id} not found", id);
        return Results.NotFound(new { Message = "Vendor not found", VendorId = id });
    }
    return Results.Ok(vendor);
});

app.MapPost("/vendors", async (IMediator mediator,
                               [FromBody] RequestVendorDto createVendorDto,
                               IValidator<RequestVendorDto> validator,
                               IMapper mapper,
                               HttpContext httpContext,
                               ILogger<Program> logger) =>
{
    var validationResult = await validator.ValidateAsync(createVendorDto);
    if (!validationResult.IsValid)
        return Results.BadRequest(validationResult.Errors);

    var correlationId = httpContext.Request.Headers["X-Correlation-Id"].ToString();
    logger.LogInformation("Creating vendor - CorrelationId: {CorrelationId}", correlationId);


    var command = mapper.Map<CreateVendorCommand>(createVendorDto);

    var vendorId = await mediator.Send(command);
    return Results.Created($"/vendors/{vendorId}", new { Id = vendorId });
});

app.MapPut("/vendors/{id:guid}", async (IMediator mediator,
                                        Guid id,
                                        [FromBody] RequestVendorDto updateVendorDto,
                                        IValidator<RequestVendorDto> validator,
                                        IMapper mapper,
                                        HttpContext httpContext,
                                        ILogger<Program> logger) =>
{
    var validationResult = await validator.ValidateAsync(updateVendorDto);
    if (!validationResult.IsValid)
        return Results.BadRequest(validationResult.Errors);

    var correlationId = httpContext.Request.Headers["X-Correlation-Id"].ToString();
    logger.LogInformation("Updating vendor {Id} - CorrelationId: {CorrelationId}", id, correlationId);

    var command = mapper.Map<UpdateVendorCommand>(updateVendorDto);
    command.Id = id;

    try
    {
        var vendorId = await mediator.Send(command);
        return Results.Ok(new { Id = vendorId });
    }
    catch (KeyNotFoundException)
    {
        return Results.NotFound(new { Message = "Vendor not found", VendorId = id });
    }
});

app.MapDelete("/vendors/{id:guid}", async (IMediator mediator, Guid id, HttpContext httpContext, ILogger<Program> logger) =>
{
    var correlationId = httpContext.Request.Headers["X-Correlation-Id"].ToString();
    logger.LogInformation("Deleting vendor {Id} - CorrelationId: {CorrelationId}", id, correlationId);

    try
    {
        await mediator.Send(new DeleteVendorCommand(id));
        return Results.NoContent();
    }
    catch (KeyNotFoundException)
    {
        return Results.NotFound(new { Message = "Vendor not found", VendorId = id });
    }
});

app.Run();