using System;
using System.Collections.Generic;
using System.Text;

namespace AndroidApp
{
    internal static class APISettings
    {
        public static readonly string BaseURL = "http://192.168.0.101:50/api/";
        
        /// там все post
        public static readonly string GetQuestionURI = "callbacks/";
        public static readonly string PutQuestionURI = "updcallbacks/";

        /// там все post
        public static readonly string GetsCameraURI = "getgoods/";
        public static readonly string GetCameraURI = "getgood/";
    }
}
