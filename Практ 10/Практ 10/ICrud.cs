namespace Практ_10
{
    internal interface ICrud
    {
        public void Create();
        public void Read(int index);
        public void Update(int index);
        public void Delete(int index);
        public void Search();
    }
}
