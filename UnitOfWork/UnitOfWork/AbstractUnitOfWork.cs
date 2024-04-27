namespace RabbitPublish.Unit
{
    public abstract class AbstractUnitOfWork<T> 
    {
        public abstract T Repositorie { get;}
        public abstract Task BegginTransaction();
        public abstract Task CommitTransaction();
        public abstract Task<int> CompleteTask();
    }
}
