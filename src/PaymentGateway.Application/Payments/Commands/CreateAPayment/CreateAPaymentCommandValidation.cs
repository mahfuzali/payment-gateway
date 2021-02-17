using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace PaymentGateway.Application.Payments.Commands.CreateAPayment
{
    public class CreateAPaymentCommandValidation : AbstractValidator<CreateAPaymentCommand>
    {
        public CreateAPaymentCommandValidation()
        {
            this.RuleFor(c => c.Card.Number)
                .NotEmpty().WithMessage("Number is required.")
                .Matches(@"^\d{8,19}$").WithMessage("Only numbers are allowed.");

            this.RuleFor(c => c.Card.Expiry)
                .NotEmpty().WithMessage("Expiry is required.")
                .Matches(@"^[0-9]{1,2}\/[0-9]{4}$").WithMessage("Invalid expiry data. Should be in MM/YYYY format");

            this.RuleFor(c => c.Card.Amount)
                .NotEmpty().WithMessage("Amount is required.");

            this.RuleFor(c => c.Card.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .Matches(@"^[A-Z]{3}$").WithMessage("Should be three character currency code, for example, GBP");

            this.RuleFor(c => c.Card.CVV)
                .NotEmpty().WithMessage("CVV is required.")
                .Matches(@"^\d{3}$").WithMessage("CVV should be three numeric characters");

        }
    }
}
