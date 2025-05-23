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
    public Event<OrderAddFailedEvent> OrderNotAddedEvent { get; private set; }
    public Event<StockReservedEvent> StockReservedEvent { get; private set; }
    public Event<StockReserveFailedEvent> StockReserveFailedEvent { get; private set; }
    public Event<PaymentCompletedEvent> PaymentCompletedEvent { get; private set; }
    public Event<PaymentFailedEvent> PaymentFailedEvent { get; private set; }
    public Event<ShipmentShippedEvent> ShipmentShippedEvent { get; private set; }
    public Event<ShipmentDeliveredEvent> ShipmentDeliveredEvent { get; private set; }
    public Event<ShipmentShipFailedEvent> ShipmentShipFailedEvent { get; private set; }
    public Event<ShipmentDeliverFailedEvent> ShipmentDeliverFailedEvent { get; private set; }

    public OrderAddSaga()
    {
        InstanceState(x => x.CurrentState);

        Event(() => OrderAddedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
        Event(() => OrderNotAddedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
        Event(() => StockReservedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
        Event(() => StockReserveFailedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
        Event(() => PaymentCompletedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
        Event(() => PaymentFailedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
        Event(() => ShipmentShippedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
        Event(() => ShipmentDeliveredEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
        Event(() => ShipmentShipFailedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
        Event(() => ShipmentDeliverFailedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));

        Initially(
            When(OrderAddedEvent)
                .ThenAsync(async context =>
                {
                    context.Saga.OrderId = OrderId.From(context.Message.OrderId);
                    context.Saga.UserId = UserId.From(context.Message.UserId);
                    context.Saga.OrderDate = context.Message.OrderDate;
                    context.Saga.CreatedAt = DateTime.UtcNow;

                    var stockReserveRequestEvent = new StockReserveRequestEvent(
                        context.Message.CorrelationId,
                        context.Message.OrderId,
                        context.Message.Items
                    );
                    
                    await context.Publish(stockReserveRequestEvent);
                })
                .TransitionTo(OrderAdded),
            When(OrderNotAddedEvent)
                .Then(context =>
                {
                    context.Saga.UserId = UserId.From(context.Message.UserId);
                    context.Saga.FailureReason = context.Message.ErrorMessage;
                    context.Saga.CreatedAt = DateTime.UtcNow;
                })
                .TransitionTo(OrderAddFailed)
        );

        During(OrderAdded,
            When(StockReservedEvent)
                .ThenAsync(async context =>
                {
                    context.Saga.OrderId = OrderId.From(context.Message.OrderId);
                    context.Saga.CreatedAt = DateTime.UtcNow;

                    var paymentCreateRequestEvent = new PaymentCreateRequestEvent(
                        context.Message.CorrelationId,
                        context.Message.OrderId
                    );
                    
                    await context.Publish(paymentCreateRequestEvent);
                })
                .TransitionTo(StockReserved),
            When(StockReserveFailedEvent)
                .Then(context =>
                {
                    context.Saga.FailureReason = context.Message.ErrorMessage;
                })
                .TransitionTo(StockReserveFailed)
        );

        During(StockReserved,
            When(PaymentCompletedEvent)
                .TransitionTo(PaymentCompleted),
            When(PaymentFailedEvent)
                .Then(context =>
                {
                    context.Saga.FailureReason = context.Message.ErrorMessage;
                })
                .TransitionTo(PaymentFailed)
        );

        During(PaymentCompleted,
            When(ShipmentShippedEvent)
                .TransitionTo(ShipmentShipped),
            When(ShipmentShipFailedEvent)
                .Then(context =>
                {
                    context.Saga.FailureReason = context.Message.ErrorMessage;
                })
                .TransitionTo(ShipmentShipFailed)
        );

        During(ShipmentShipped,
            When(ShipmentDeliveredEvent)
                .Then(context =>
                {
                    context.Saga.CompletedAt = DateTime.UtcNow;
                })
                .TransitionTo(Completed),
            When(ShipmentDeliverFailedEvent)
                .Then(context =>
                {
                    context.Saga.FailureReason = context.Message.ErrorMessage;
                })
                .TransitionTo(ShipmentDeliverFailed)
        );
    }
}