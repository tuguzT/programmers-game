namespace Field
{
    public interface IField
    {
        uint Width { get; }

        Chunk[,] Generate();
    }
}
