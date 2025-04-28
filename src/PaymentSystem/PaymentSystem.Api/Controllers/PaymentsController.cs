using Common.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Application.Queries.Payments.Common;
using PaymentSystem.Application.Queries.Payments.Details;
using PaymentSystem.Application.Queries.PaymentTransactions.Common;
using PaymentSystem.Application.Queries.PaymentTransactions.Details;

namespace PaymentSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ApiController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentResponse>> GetPayment(int id)
        => await Send(new PaymentDetailsQuery { Id = id });

    [HttpGet("transactions/{paymentId}")]
    public async Task<ActionResult<List<PaymentTransactionResponse>>> GetPaymentTransactions(int paymentId)
        => await Send(new PaymentTransactionDetailsQuery { PaymentId = paymentId });
}