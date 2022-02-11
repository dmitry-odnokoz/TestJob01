namespace TestJob01.ApplicationCore.Entities.TransferAggregate {
    /// <summary>
    /// Базовый класс для перемщения номенклатуры и ее сторнирования
    /// </summary>
    public abstract class TransferBase: BaseEntity {
        private readonly TransferKid _kid = default!;
        /// <summary>
        /// Отправитель
        /// </summary>
        public Warehouse Shipper { get; init; } = default!;
        /// <summary>
        /// Получатель
        /// </summary>
        public Warehouse Receiver { get; init; } = default!;
        public DateTimeOffset OperationDate { get; init; } = default!;
        private readonly List<TransferItem> _transferItems = new();

        public IReadOnlyCollection<TransferItem> TransferItems => _transferItems.AsReadOnly();

        protected TransferBase(
            Guid id,
            TransferKid transferKid,
            Warehouse shipper,
            Warehouse receiver,
            DateTimeOffset operationDate,
            IEnumerable<TransferItem> items) {
            Id = id;
            _kid = transferKid;
            Shipper = shipper;
            Receiver = receiver;
            OperationDate = operationDate;
            _transferItems.AddRange(items);
        }

        protected TransferBase() : base() { } // required by EF
    }
}
