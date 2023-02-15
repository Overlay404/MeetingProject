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

namespace MeetingProjectTestApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {

    public string TextMd
    {
        get { return (string)GetValue(TextMdProperty); }
        set { SetValue(TextMdProperty, value); }
    }

    public static readonly DependencyProperty TextMdProperty =
        DependencyProperty.Register("TextMd", typeof(string), typeof(MainWindow));

        public MainWindow()
        {
            TextMd = "# Dillinger\r\n## _The Last Markdown Editor, Ever_\r\n\r\n![N|Solid](https://www.meme-arsenal.com/memes/1149c9ca60ff2a44189b632eb762bd1e.jpg)\r\n\r\n[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)\r\n\r\nDillinger is a cloud-enabled, mobile-ready, offline-storage compatible,\r\nAngularJS-powered HTML5 Markdown editor.\r\n\r\n- Type some Markdown on the left\r\n - See HTML in the right\r\n - ✨Magic ✨\r\n\r\n ## Features\r\n\r\n - Import a HTML file and watch it magically convert to Markdown\r\n - Drag and drop images (requires your Dropbox account be linked)\r\n - Import and save files from GitHub, Dropbox, Google Drive and One Drive\r\n - Drag and drop markdown and HTML files into Dillinger\r\n - Export documents as Markdown, HTML and PDF\r\n\r\n Markdown is a lightweight markup language based on the formatting conventions\r\n that people naturally use in email.\r\n As [John Gruber] writes on the [Markdown site][df1]\r\n\r\n > The overriding design goal for Markdown's\r\n > formatting syntax is to make it as readable\r\n > as possible. The idea is that a\r\n > Markdown-formatted document should be\r\n > publishable as-is, as plain text, without\r\n > looking like it's been marked up with tags\r\n > or formatting instructions.\r\n\r\n This text you see here is *actually- written in Markdown! To get a feel\r\n for Markdown's syntax, type some text into the left window and\r\n watch the results in the right.\r\n\r\n ## Tech\r\n\r\n Dillinger uses a number of open source projects to work properly:\r\n\r\n - [AngularJS] - HTML enhanced for web apps!\r\n - [Ace Editor] - awesome web-based text editor\r\n - [markdown-it] - Markdown parser done right. Fast and easy to extend.\r\n - [Twitter Bootstrap] - great UI boilerplate for modern web apps\r\n - [node.js] - evented I/O for the backend\r\n - [Express] - fast node.js network app framework [@tjholowaychuk]\r\n - [Gulp] - the streaming build system\r\n - [Breakdance](https://breakdance.github.io/breakdance/) - HTML\r\n to Markdown converter\r\n - [jQuery] - duh\r\n\r\n And of course Dillinger itself is open source with a [public repository][dill]\r\n  on GitHub.\r\n\r\n ## Installation\r\n\r\n Dillinger requires [Node.js](https://nodejs.org/) v10+ to run.\r\n\r\n Install the dependencies and devDependencies and start the server.\r\n\r\n ```sh\r\n cd dillinger\r\n npm i\r\n node app\r\n ```\r\n\r\n For production environments...\r\n\r\n        ```sh\r\n        npm install --production\r\n        NODE_ENV=production node app\r\n        ```\r\n\r\n        ## Plugins\r\n\r\n        Dillinger is currently extended with the following plugins.\r\n        Instructions on how to use them in your own application are linked below.\r\n\r\n        | Plugin | README |\r\n        | ------ | ------ |\r\n        | Dropbox | [plugins/dropbox/README.md][PlDb] |\r\n        | GitHub | [plugins/github/README.md][PlGh] |\r\n        | Google Drive | [plugins/googledrive/README.md][PlGd] |\r\n        | OneDrive | [plugins/onedrive/README.md][PlOd] |\r\n        | Medium | [plugins/medium/README.md][PlMe] |\r\n        | Google Analytics | [plugins/googleanalytics/README.md][PlGa] |\r\n\r\n        ## Development\r\n\r\n        Want to contribute? Great!\r\n\r\n        Dillinger uses Gulp + Webpack for fast developing.\r\n        Make a change in your file and instantaneously see your updates!\r\n\r\n        Open your favorite Terminal and run these commands.\r\n\r\n        First Tab:\r\n\r\n        ```sh\r\n        node app\r\n        ```\r\n\r\n        Second Tab:\r\n\r\n        ```sh\r\n        gulp watch\r\n        ```\r\n\r\n        (optional) Third:\r\n\r\n        ```sh\r\n        karma test\r\n        ```\r\n\r\n        #### Building for source\r\n\r\n        For production release:\r\n\r\n        ```sh\r\n        gulp build --prod\r\n        ```\r\n\r\n        Generating pre-built zip archives for distribution:\r\n\r\n        ```sh\r\n        gulp build dist --prod\r\n        ```\r\n\r\n        ## Docker\r\n\r\n        Dillinger is very easy to install and deploy in a Docker container.\r\n\r\n        By default, the Docker will expose port 8080, so change this within the\r\n        Dockerfile if necessary. When ready, simply use the Dockerfile to\r\n        build the image.\r\n\r\n        ```sh\r\n        cd dillinger\r\n        docker build -t /dillinger:${package.json.version} .\r\n        ```\r\n\r\n        This will create the dillinger image and pull in the necessary dependencies.\r\n        Be sure to swap out `${package.json.version}` with the actual\r\n        version of Dillinger.\r\n\r\n        Once done, run the Docker image and map the port to whatever you wish on\r\n        your host. In this example, we simply map port 8000 of the host to\r\n        port 8080 of the Docker (or whatever port was exposed in the Dockerfile):\r\n\r\n        ```sh\r\n        docker run -d -p 8000:8080 --restart=always --cap-add=SYS_ADMIN --name=dillinger /dillinger:${package.json.version}\r\n        ```\r\n\r\n        > Note: `--capt-add=SYS-ADMIN` is required for PDF rendering.\r\n\r\n        Verify the deployment by navigating to your server address in\r\n        your preferred browser.\r\n\r\n        ```sh\r\n        127.0.0.1:8000\r\n        ```\r\n\r\n        ## License\r\n\r\n        MIT\r\n\r\n        **Free Software, Hell Yeah!**\r\n\r\n        [//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)\r\n";
            InitializeComponent();

        }
    }
}
