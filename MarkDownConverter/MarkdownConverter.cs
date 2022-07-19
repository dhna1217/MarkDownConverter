using MarkDownConverter.Parser;
using MarkDownConverter.Parser.Inline;
using System.Text;

namespace MarkDownConverter;

public class MarkdownConverter
{
    private StringBuilder _finalText;

    public MarkdownConverter()
    {        
        _finalText = new StringBuilder(); //Final result
    }

    /// <summary>
    /// Read line by line from input and parse converted html text
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public string Parse(IEnumerable<string> input)
    {
        Block prevBlock = new Block(BlockType.None);

        foreach (var line in input)
        {
            var processed = line.TrimStart(); //Trim any whitespace at the beginning of a line

            if(string.IsNullOrEmpty(processed)) //If the line is empty, always process the existing block(if exists)
            {
                ProcessPreviousBlock(prevBlock);
                prevBlock = new Block(BlockType.None);
            }
            else
            {
                processed = Anchor.Process(ParserType.RegularExpression, processed); //Process all anchor tags

                if(processed.StartsWith("# "))
                {
                    ProcessPreviousBlock(prevBlock);

                    Block block = new Block(BlockType.HeadingOne);
                    block.Add(processed);

                    _finalText.Append($"{Process(block)}\n");

                    prevBlock = block;
                }
                else if (processed.StartsWith("## "))
                {
                    ProcessPreviousBlock(prevBlock);

                    Block block = new Block(BlockType.HeadingTwo);
                    block.Add(processed);

                    _finalText.Append($"{Process(block)}\n");

                    prevBlock = block;
                }
                else if (processed.StartsWith("### "))
                {
                    ProcessPreviousBlock(prevBlock);

                    Block block = new Block(BlockType.HeadingThree);
                    block.Add(processed);

                    _finalText.Append($"{Process(block)}\n");

                    prevBlock = block;
                }
                else if (processed.StartsWith("#### "))
                {
                    ProcessPreviousBlock(prevBlock);

                    Block block = new Block(BlockType.HeadingFour);
                    block.Add(processed);

                    _finalText.Append($"{Process(block)}\n");

                    prevBlock = block;
                }
                else if (processed.StartsWith("##### "))
                {
                    ProcessPreviousBlock(prevBlock);

                    Block block = new Block(BlockType.HeadingFive);
                    block.Add(processed);

                    _finalText.Append($"{Process(block)}\n");

                    prevBlock = block;
                }
                else if (processed.StartsWith("###### "))
                {
                    ProcessPreviousBlock(prevBlock);

                    Block block = new Block(BlockType.HeadingSix);
                    block.Add(processed);

                    _finalText.Append($"{Process(block)}\n");

                    prevBlock = block;
                }
                else     //If not heading, it's always <p></p>
                {
                    if(prevBlock.Content.Length > 0)
                    {
                        prevBlock.Add($"\n{processed}");
                    }
                    else
                    {
                        Block block = new Block(BlockType.Paragraph);
                        block.Add(processed);

                        prevBlock = block;
                    }
                }
            }
        }
        ProcessPreviousBlock(prevBlock);

        return _finalText.ToString();
    }

    private void ProcessPreviousBlock(Block block)
    {
        if (block.Content.Length > 0)
        {
            var processed = Process(block);
            _finalText.Append($"{processed}");
        }
    }

    
    // Process lines in block and return converted html
    private string Process(Block block)
    {
        var processedHtml = string.Empty;

        if (block.BlockType == BlockType.HeadingOne)
        {
            processedHtml = Heading.ConvertToHtml(1, block.Content.Substring(2));
        }
        else if (block.BlockType == BlockType.HeadingTwo)
        {
            processedHtml = Heading.ConvertToHtml(2, block.Content.Substring(3));
        }
        else if (block.BlockType == BlockType.HeadingThree)
        {
            processedHtml = Heading.ConvertToHtml(3, block.Content.Substring(4));
        }
        else if (block.BlockType == BlockType.HeadingFour)
        {
            processedHtml = Heading.ConvertToHtml(4, block.Content.Substring(5));
        }
        else if (block.BlockType == BlockType.HeadingFive)
        {
            processedHtml = Heading.ConvertToHtml(5, block.Content.Substring(6));
        }
        else if (block.BlockType == BlockType.HeadingSix)
        {
            processedHtml = Heading.ConvertToHtml(6, block.Content.Substring(7));
        }
        else
        {
            processedHtml = Paragraph.ConvertToHtml(block.Content.ToString());
        }

        block.Clear();

        return processedHtml;
    }
}
