using Markdig.Helpers;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MeetingProject.Model;
using MeetingProject.View.Windows;
using MeetingProject.View.UserControls;
using System.Windows.Media.Animation;

namespace MeetingProject.View.Pages
{
    /// <summary>
    /// Страница редактирования содержимого проекта
    /// </summary>
    public partial class EditingProjectPage : Page
    {
        public static EditingProjectPage Instance { get; private set; }

        public EditingProjectPage(Project project = null)
        {
            MdText = project.text;
            ProjectObject = project;

            InitializeComponent();
            MarkdownConvertingAndShowingInBrowser();

            Instance = this;

            MarkdownText.TextChanged += delegate (object sender, TextChangedEventArgs e) { MarkdownConvertingAndShowingInBrowser(); };
        }

        private void MarkdownConvertingAndShowingInBrowser()
        {
            var pipeline = new MarkdownPipelineBuilder()
                                    .UseAdvancedExtensions()
                                    .UsePipeTables()
                                    .UseSoftlineBreakAsHardlineBreak()
                                    .UseCustomContainers()
                                    .Build();

            HTMLTextInPreview = Markdown.ToHtml(MdText, pipeline);
            BrowserForPreviewMarkdown.NavigateToString(HTMLHeader);
        }

        private void BrowserForPreviewMarkdown_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (e.Uri != null)
            {
                MessageControl.Instance.StartAnimation();
                ProjectObject.text = MdText;
                e.Cancel = true;
                System.Windows.Clipboard.SetText(e.Uri.ToString());
            }
        }

        public void AcceptChanges()
        {
            ProjectObject.text = MdText;
            App.db.SaveChanges();
        }
    }
}
