using BoxesApi.Models;
using FluentValidation;

namespace BoxesApi.Validators
{
    public class LeadValidator : AbstractValidator<Lead>
    {
        public LeadValidator()
        {
            RuleFor(x => x.PlaceId).NotEmpty();
            RuleFor(x => x.AppointmentAt).NotEmpty();
            RuleFor(x => x.ServiceType).NotEmpty().Must(x =>
                new[] { "cambio_aceite", "rotacion_neumaticos", "otro" }.Contains(x));

            RuleFor(x => x.Contact).NotNull();
            RuleFor(x => x.Contact.Name).NotEmpty();
            RuleFor(x => x.Contact.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Contact.Phone).NotEmpty();

            When(x => x.Vehicle != null, () =>
            {
                RuleFor(x => x.Vehicle!.LicensePlate).NotEmpty();
            });
        }
    }
}
