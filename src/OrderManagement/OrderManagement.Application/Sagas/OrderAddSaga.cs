using Common.Domain.Events.OrderManagements;
using Common.Domain.Events.PaymentSystems;
using Common.Domain.Events.Stocks;
using MassTransit;
using Common.Domain.Events.Shippings;
using Common.Domain.ValueObjects;

namespace OrderManagement.Application.Sagas;

public class OrderAddSaga : MassTransitStateMachine<OrderAddState>
{
    //States
    public State OrderAdded { get; private set; }
    public State StockReserved { get; private set; }
    public State PaymentCompleted { get; private set; }
    public State ShipmentShipped { get; private set; }
    public State ShipmentDelivered { get; private set; }
    public State Completed { get; private set; }

    // Failed States
    public State OrderAddFailed { get; private set; }
    public State StockReserveFailed { get; private set; }
    public State PaymentFailed { get; private set; }
    public State ShipmentShipFailed { get; private set; }
    public State ShipmentDeliverFailed { get; private set; }

    // Integration Events
    public Event<OrderAddedEvent> OrderAddedEvent { get; private set; }
    public Event<StockReservedEvent> StockReservedEvent { get; private set; }
    public Event<PaymentCompletedEvent> PaymentCompletedEvent { get; private set; }
    public Event<ShipmentShippedEvent> ShipmentShippedEvent { get; private set; }
    public Event<ShipmentDeliveredEvent> ShipmentDeliveredEvent { get; private set; }

    public OrderAddSaga()
    {
        InstanceState(x => x.CurrentState);

        Event(() => OrderAddedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
        Event(() => StockReservedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
        Event(() => PaymentCompletedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
        Event(() => ShipmentShippedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
        Event(() => ShipmentDeliveredEvent, x => x.CorrelateById(context => context.Message.CorrelationId));

        Initially(
            When(OrderAddedEvent)
                .ThenAsync(async context =>
                {
                    context.Saga.OrderId = context.Message.OrderId;
                    context.Saga.UserId = context.Message.UserId;
                    context.Saga.OrderDate = context.Message.OrderDate;
                    context.Saga.CreatedAt = DateTime.UtcNow;

                    var stockReserveRequestEvent = new StockReserveRequestEvent(
                        context.Message.CorrelationId,
                        context.Message.OrderId,
                        context.Message.Items
                    );
                    
                    await context.Publish(stockReserveRequestEvent);
                })
                .TransitionTo(OrderAdded)
        );

        During(OrderAdded,
            When(StockReservedEvent)
                .ThenAsync(async context =>
                {
                    context.Saga.OrderId = context.Message.OrderId;
                    context.Saga.CreatedAt = DateTime.UtcNow;

                    var paymentCreateRequestEvent = new PaymentCreateRequestEvent(
                        context.Message.CorrelationId,
                        context.Message.OrderId
                    );
                    
                    await context.Publish(paymentCreateRequestEvent);
                })
                .TransitionTo(StockReserved)
        );

        During(StockReserved,
            When(PaymentCompletedEvent)
                .TransitionTo(PaymentCompleted)
        );

        During(PaymentCompleted,
            When(ShipmentShippedEvent)
                .TransitionTo(ShipmentShipped)
        );

        During(ShipmentShipped,
            When(ShipmentDeliveredEvent)
                .Then(context =>
                {
                    context.Saga.CompletedAt = DateTime.UtcNow;
                })
                .TransitionTo(Completed)
        );
    }
}