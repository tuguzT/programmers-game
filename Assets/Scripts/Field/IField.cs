using Model;

namespace Field
{
    public interface IField
    {
        ChunkData[,] Generate();
    }
}
