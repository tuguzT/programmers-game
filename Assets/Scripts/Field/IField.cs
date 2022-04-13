namespace Field
{
    public interface IField
    {
        uint Width { get; }

        ChunkData[,] Generate();
    }
}
