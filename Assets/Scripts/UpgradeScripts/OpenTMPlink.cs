using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UpgradeScripts
{
    public class OpenTMPlink : MonoBehaviour, IPointerClickHandler
    {
        private TMP_Text textMessage;
        private const string privacyURL = "https://imageconverter.io/privacy-policy";
        private const string tosURL = "https://imageconverter.io/terms-of-use";

        private void Start()
        {
            textMessage = GetComponent<TMP_Text>();
            CheckLinks ();
        }

        // Check links in text 
        void CheckLinks () {  
            Regex regx = new Regex ("((http://|https://|www\\.)([A-Z0-9.-:]{1,})\\.[0-9A-Z?;~&#=\\-_\\./]{2,})" , RegexOptions.IgnoreCase | RegexOptions.Singleline);   
            MatchCollection matches = regx.Matches (textMessage.text);   
            foreach (Match match in matches)    
                textMessage.text = textMessage.text.Replace (match.Value, ShortLink(match.Value));       
        }  
        // Cut long url 
        string ShortLink (string link) {  
            string text = link;  
            int left = 9;     
            int right = 16;     
            string cut = "...";    
            if (link.Length > (left + right + cut.Length))    
                text = $"{link.Substring(0, left)}{cut}{link.Substring(link.Length - right, right)}";  
            return $"<#7f7fe5><u><link=\"{link}\">{text}</link></u></color>"; 
        }
        
        // Get link and open page 
        public void OnPointerClick (PointerEventData eventData) {  
            int linkIndex = TMP_TextUtilities.FindIntersectingLink (textMessage, eventData.position, eventData.pressEventCamera);  
            if (linkIndex == -1)    
                return;  
            TMP_LinkInfo linkInfo = textMessage.textInfo.linkInfo[linkIndex];
            string selectedLink = linkInfo.GetLinkID();  
            if (selectedLink == "privacy") {   
                Debug.LogFormat ("Open link {0}", selectedLink);
                Application.OpenURL (privacyURL);          
            } 
            else if (selectedLink == "tos")
            {
                Debug.LogFormat ("Open link {0}", selectedLink);
                Application.OpenURL(tosURL);
            }
        }
    }
}