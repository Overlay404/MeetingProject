using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Dom.Events;
using AngleSharp.Html.Dom;
using AngleSharp.Io;
using Markdig;
using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MeetingProjectTestApplication
{
    public class ImageBytePatternParser : BlockParser
    {
        public Regex RegexNumberInImage = new Regex(@"\d+");

        public override BlockState TryOpen(BlockProcessor processor)
        {
            return BlockState.None;

           

        }

        public override BlockState TryContinue(BlockProcessor processor, Block block)
        {
            // Add custom parsing logic to continue processing your pattern here
            throw new System.NotImplementedException();
        }
    }
}
