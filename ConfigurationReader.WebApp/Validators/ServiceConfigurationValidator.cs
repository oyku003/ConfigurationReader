using ConfigurationReader.Shared.Enums;
using ConfigurationReader.Shared.Models.Dtos;
using ConfigurationReader.WebApp.Helpers;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System;
using System.Net;
using System.Reflection;
using System.ComponentModel;
using ConfigurationReader.WebApp.Enums;
using Type = ConfigurationReader.Shared.Enums.Type;

namespace ConfigurationReader.WebApp.Validators
{
    public class ServiceConfigurationValidator : AbstractValidator<ServiceConfigurationDto>
    {
        public string NotEmptyMessage { get; } = "{PropertyName} alanı boş olamaz.";
        public ServiceConfigurationValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(NotEmptyMessage);
            RuleFor(x => x.Value).NotEmpty().WithMessage(NotEmptyMessage).When(x => x.Type == Type.Boolean.ToString()).IsInEnum().WithMessage("{PropertyName} alanı True ya da False olmalı");
            RuleFor(x => x.Type).NotEmpty().WithMessage(NotEmptyMessage).Must(x =>  x.HasDescriptionByEnum<Type>()).WithMessage("{PropertyName} alanı Int, String, Double ya da Boolean olmalı");
            RuleFor(x => x.ApplicationName).NotEmpty().WithMessage(NotEmptyMessage);

           
        }

    }
}
