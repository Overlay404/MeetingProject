﻿using MeetingProject.Model;
using System.Text.RegularExpressions;
using System.Windows;

namespace MeetingProject.View.Pages
{
    partial class EditingProjectPage
    {
        public static string HTMLTextInPreview = "";

        public Regex regexForImageProcessing = new Regex(@"%\d+image");

        public Project ProjectObject { get; private set; }

        public string MdText
        {
            get { return (string)GetValue(MdTextProperty); }
            set { SetValue(MdTextProperty, value); }
        }

        public static readonly DependencyProperty MdTextProperty =
            DependencyProperty.Register("MdText", typeof(string), typeof(EditingProjectPage));

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

