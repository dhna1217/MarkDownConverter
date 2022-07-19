using Xunit;
using System.Collections.Generic;
using MarkDownConverter.Parser.Inline;

namespace MarkDownConverter.UnitTests
{
    public class UnitTest
    {

        [Fact]
        public void Convert_Heading()
        {
            MarkdownConverter conv = new MarkdownConverter();
            List<string> text = new List<string>();
            text.Add("# Heading One");
            text.Add("## Heading Two");
            text.Add("### Heading Three");
            text.Add("#### Heading Four");
            text.Add("##### Heading Five");
            text.Add("###### Heading Six");

            var expected = "<h1>Heading One</h1>\n<h2>Heading Two</h2>\n<h3>Heading Three</h3>\n<h4>Heading Four</h4>\n<h5>Heading Five</h5>\n<h6>Heading Six</h6>\n";
            var result = conv.Parse(text);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Convert_Heading_With_Edge_Cases()
        {
            MarkdownConverter conv = new MarkdownConverter();
            List<string> text = new List<string>();
            text.Add("# [Test](http://test.com)");
            text.Add("   # Heading One");
            text.Add("#    Heading One");
            text.Add("# ");

            var expected = "<h1><a href=\"http://test.com\">Test</a></h1>\n<h1>Heading One</h1>\n<h1>Heading One</h1>\n<h1></h1>\n";
            var result = conv.Parse(text);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Convert_Heading_Invalid_Case()
        {
            MarkdownConverter conv = new MarkdownConverter();
            List<string> text = new List<string>();
            text.Add("#Not Heading");

            var expected = "<p>#Not Heading</p>\n";
            var result = conv.Parse(text);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Convert_Paragraph_With_Anchor_And_NewLine()
        {
            MarkdownConverter conv = new MarkdownConverter();
            List<string> text = new List<string>();
            text.Add("This is line one.");
            text.Add("This is line two.");
            text.Add("\n");
            text.Add("This with anchor [Test](http://test.com)");

            var expected = "<p>This is line one.\nThis is line two.</p>\n<p>This with anchor <a href=\"http://test.com\">Test</a></p>\n";
            var result = conv.Parse(text);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Convert_Empty_String()
        {
            MarkdownConverter conv = new MarkdownConverter();
            List<string> text = new List<string>();
            text.Add("");

            var expected = "";
            var result = conv.Parse(text);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Convert_Anchor_Valid_Empty_Link()
        {
            var test = "[Test]()";
            var expected = "<a href=\"\">Test</a>";
            var result = Anchor.Process(ParserType.RegularExpression, test);

            Assert.Equal(expected, result);
        }
       
        [Fact]
        public void Convert_Anchor_Invalid_Url_Space() //Should we allow this to be converted to anchor?
        {
            var test = "[Test](http://te  st.com)";
            var expected = "[Test](http://te  st.com)";

            var result = Anchor.Process(ParserType.RegularExpression, test);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Convert_Anchor_Invalid_Empty_Text()
        {
            var test = "[](www.test.com)";
            var expected = "[](www.test.com)";

            var result = Anchor.Process(ParserType.RegularExpression, test);

            Assert.Equal(expected, result);
        }
    }
}