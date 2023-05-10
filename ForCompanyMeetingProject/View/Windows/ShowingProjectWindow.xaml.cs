using ForCompanyMeetingProject.Model;
using Markdig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ForCompanyMeetingProject.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для ShowingProjectWindow.xaml
    /// </summary>
    public partial class ShowingProjectWindow : Window
    {
        public static string HTMLTextInPreview = "";

        public ShowingProjectWindow(Project project = null)
        {
            InitializeComponent();
            MarkdownConvertingAndShowingInBrowser(project);
        }

        private void MarkdownConvertingAndShowingInBrowser(Project project)
        {
            var pipeline = new MarkdownPipelineBuilder()
                                    .UseAdvancedExtensions()
                                    .UsePipeTables()
                                    .UseSoftlineBreakAsHardlineBreak()
                                    .UseCustomContainers()
                                    .Build();

            HTMLTextInPreview = Markdown.ToHtml(project.text, pipeline);
            BrowserForPreviewMarkdown.NavigateToString(HTMLHeader);
        }

        private void BrowserForPreviewMarkdown_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (e.Uri != null)
            {
                e.Cancel = true;
                Clipboard.SetText(e.Uri.ToString());
            }
        }

        public string HTMLHeader
        {
            set => HTMLHeader = value;
            get => $@"<html>
<head>
    <meta http-equiv='Content-Type' content='text/html;charset=utf-8'>
    <style>
        @import url('https://fonts.googleapis.com/css?family=Special+Elite');

        body {{
            font-family: 'Hubot Sans', sans-serif;
            font-size: 12px;
        }}
        pre{{
            overflow-x:scroll;
        }}
        image{{
            width: 100%;
        }}
        code {{
            font-family: 'Source Code Pro', monospace;
            background-color: black;
            color: rgb(211, 211, 211);
            font-size: 15px;
            border: 1px solid grey;
            padding: 5px 10px 5px 10px;
            white-space: nowrap;
        }}
        blockquote {{
            border-left: 4px;
			border-left-style: solid;
			border-left-color: #044AFD;
    		padding: 8px 0 8px 20px;
			background: #d6d6ff; 
			font - family: cursive;
	        color: #2c2c2c;
	        font-size: 15px;
	        max-width: 600px;
	        margin: 0;
        }}
        table {{
            width: 100%;
	        margin-bottom: 20px;
	        border: 1px solid #dddddd;
	        border-collapse: collapse; 
        }}
        th {{
            font - weight: bold;
	        padding: 5px;
	        background: #efefef;
	        border: 1px solid #dddddd;
        }}
        td {{
            border: 1px solid #dddddd;
	        padding: 5px;
        }}
    </style>
</head>
<body>
    {HTMLTextInPreview}
</body>
</html>";
        }
    }
}
