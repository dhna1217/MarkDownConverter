using System.Text;

namespace MarkDownConverter.Parser;

public class Block
{
    private StringBuilder _blockContent;

    public BlockType BlockType { get; private set; }
    
    public string Content { get { return _blockContent.ToString(); } }

    public Block(BlockType blockType)
    {
        _blockContent = new StringBuilder();
        BlockType = blockType;
    }

    public void Add(string line)
    {
        _blockContent.Append(line);
    }

    public void Clear()
    {        
        _blockContent.Clear();
    }
}




