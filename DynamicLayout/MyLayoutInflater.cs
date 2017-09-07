using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Interop;
using JObject = Java.Lang.Object;
using Org.XmlPull.V1;
using Android.Util;
using System.Xml;
using Java.IO;

namespace DynamicLayout
{
    public class MyLayoutInflater : Java.Lang.Object
    {
        //[Register("inflate", "(ILandroid/view/ViewGroup;)Landroid/view/View;", "GetInflate_ILandroid_view_ViewGroup_Handler")]
        //public virtual unsafe View Inflate(int resource, ViewGroup root)
        //{
        //    JniArgumentValue* parameters = (JniArgumentValue*) stackalloc byte[(((IntPtr)2) * sizeof(JniArgumentValue))];
        //    parameters[0] = new JniArgumentValue(resource);
        //    parameters[1] = new JniArgumentValue((root == null) ? IntPtr.Zero : root.Handle);
        //    return JObject.GetObject<View>(_members.InstanceMethods.InvokeVirtualObjectMethod("inflate.(ILandroid/view/ViewGroup;)Landroid/view/View;", this, parameters).Handle, JniHandleOwnership.TransferLocalRef);
        //}

        //public XmlPullParser GetXmlPullParser(string resource)
        //{

        //    XmlPullParser parser = XmlReaderPullParser.ToLocalJniHandle Xml.NewPullParser().lo;.newPullParser();
        //    try
        //    {
        //        FileInputStream fs = new FileInputStream(resource);
        //        parser.setInput(fs, "utf-8");
        //    }
        //    catch (Exception e)
        //    {
        //        e.printStackTrace();
        //    }
        //    return parser;
        //}
    }
}