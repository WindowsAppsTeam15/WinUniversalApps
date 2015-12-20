namespace YamAndRateApp.Utils
{
    using System;
    using Windows.Data.Xml.Dom;
    using Windows.UI.Notifications;

    public class ToastManager
    {
        private ToastTemplateType toastTemplate;
        private XmlDocument toastXml;

        public ToastManager()
        {
            this.toastTemplate = ToastTemplateType.ToastImageAndText02;
            this.toastXml = ToastNotificationManager.GetTemplateContent(this.toastTemplate);
        }

        public void CreateToast(string heading, string content, string image, string navigateTo)
        {
            this.FillToastContent(heading, content, image, navigateTo);
            ToastNotification toast = new ToastNotification(this.toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        public void CreateToast(string heading, string image)
        {
            this.FillToastContent(heading, image);
            ToastNotification toast = new ToastNotification(this.toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        private void FillToastContent(string heading, string image)
        {
            // Fill in the text elements
            XmlNodeList stringElements = this.toastXml.GetElementsByTagName("text");
            stringElements[0].AppendChild(this.toastXml.CreateTextNode(heading));

            // Specify the absolute path to an image
            XmlNodeList imageElements = this.toastXml.GetElementsByTagName("image");
            imageElements[0].Attributes.GetNamedItem("src").NodeValue = image;

            var toastElement = ((XmlElement)toastXml.SelectSingleNode("/toast"));
        }

        private void FillToastContent(string heading, string content, string image, string navigateTo)
        {
            // Fill in the text elements
            XmlNodeList stringElements = this.toastXml.GetElementsByTagName("text");
            stringElements[0].AppendChild(this.toastXml.CreateTextNode(heading));
            stringElements[1].AppendChild(this.toastXml.CreateTextNode(content));

            // Specify the absolute path to an image
            XmlNodeList imageElements = this.toastXml.GetElementsByTagName("image");
            imageElements[0].Attributes.GetNamedItem("src").NodeValue = image;
            
            var toastElement = ((XmlElement)toastXml.SelectSingleNode("/toast"));
            toastElement.SetAttribute("launch", navigateTo);
        }
    }
}
