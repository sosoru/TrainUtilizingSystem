namespace Tus.AutoController.Deserialized
{
    public class DeserializedVehicle
    {
        public DeserializedVehicle()
        {
            this.Name = "";
            this.ShownName = "";
            this.CurrentBlock = new DeserializedBlock();
            this.Halt = new object[] { };
            this.ReallocatableBlockNames = new string[] { };

        }

        public float Accelation { get; set; }
        public virtual DeserializedBlock CurrentBlock { get; set; }
        public virtual float CurrentSpeed { get; set; }
        public virtual int Distance { get; set; }
        public object[] Halt { get; set; }
        public bool IsHalted { get; set; }
        public int Length { get; set; }
        public string Name { get; set; }
        public string[] ReallocatableBlockNames { get; set; }
        public string ShownName { get; set; }
        public virtual float Speed { get; set; }
        public int StopThreshold { get; set; }
        public int VehicleID { get; set; }
    }
    public class DeserializedBlock
    {
        public DeserializedBlock()
        {
            this.Name = "";
        }
        public virtual string Name { get; set; }
    }
}