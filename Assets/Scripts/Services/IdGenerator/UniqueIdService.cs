namespace Services.IdGenerator
{
    public class UniqueIdService : IUniqueIdService
    {
        private int _current;

        public UniqueIdService() => 
            _current = 0;

        public int GetNextId() => 
            ++_current;
    }
}