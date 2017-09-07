using Android.App;
using Android.Widget;
using Android.OS;
using System.Xml;
using Android.Util;
using Android.Views;
using Org.Json;
using Json2View;
using Java.Lang;
using Java.IO;
using Android.Content;
using System.IO;
using Android.Net;
using Android.Runtime;
using System;

namespace DynamicLayout
{
    [Activity(Label = "DynamicLayout", MainLauncher = true)]
    public class MainActivity : Activity,View.IOnClickListener
    {
        public void OnClick(View v)
        {
            //StartActivity(new Intent( Intent.ActionView, Uri.Parse("http://www.avocarrot.com/")));
        }

        void TestJniInvocation(Context context,JSONObject jSONObject,Class cs)
        {
            IntPtr DynamicViewClass = JNIEnv.FindClass("com/avocarrot/json2view/DynamicView");
            if (DynamicViewClass == IntPtr.Zero)
                throw new InvalidOperationException("Couldn't find mono.android.test.Adder");
            IntPtr createViewMethod = JNIEnv.GetStaticMethodID(DynamicViewClass, "createView", "(Lcom/avocarrot/json2view/DynamicView;II)I");
            IntPtr result = JNIEnv.CallStaticObjectMethod(DynamicViewClass, createViewMethod, new JValue(this),new JValue(jSONObject), new JValue(cs));
            View view = Java.Lang.Object.GetObject<View>(result, JniHandleOwnership.TransferLocalRef);
            string s = "";
            //IntPtr Adder_ctor = JNIEnv.GetMethodID(DynamicViewClass, "<init>", "()V");
            //if (Adder_ctor == IntPtr.Zero)
            //    throw new InvalidOperationException("Couldn't find mono.android.test.Adder.#ctor()");
            //IntPtr Adder_add = JNIEnv.GetMethodID(DynamicViewClass, "add", "(II)I");
            //if (Adder_add == IntPtr.Zero)
            //    throw new InvalidOperationException("Couldn't find mono.android.test.Adder.add(int,int)");
            //IntPtr instance = JNIEnv.NewObject(DynamicViewClass, Adder_ctor);
            //int result = JNIEnv.CallIntMethod(instance, Adder_add, new JValue(2), new JValue(3));
            //textview.Text += "\n\nnew Adder().add(2,3)=" + result;

            //var boundAdder = new Adder(instance, JniHandleOwnership.DoNotTransfer);
            //if (boundAdder.Add(3, 4) != 7)
            //    throw new InvalidOperationException("Add(3,4) != 7!");
            //JNIEnv.DeleteLocalRef(instance);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            JSONObject jsonObject;

            try
            {

                jsonObject = new JSONObject(readFile("sample.json", this));

            }
            catch (JSONException je)
            {
                je.PrintStackTrace();
                jsonObject = null;
            }
            TestJniInvocation(this, jsonObject, null);
            //if (jsonObject != null)
            //{

            //    /* create dynamic view and return the view with the holder class attached as tag */
            //    View sampleView = DynamicView.createView(this, jsonObject, Java.Lang.Class.FromType( typeof(SampleViewHolder)));
            //    //View sampleView = DynamicView.createView(this, jsonObject, typeof(SampleViewHolder));
            //    /* get the view with id "testClick" and attach the onClickListener */
            //    //((SampleViewHolder) sampleView.Tag).clickableView.SetOnClickListener(this);
            //    //ViewGroup.LayoutParams layoutParams = new ViewGroup.LayoutParams(this, new LayoutAttribute());
            //    sampleView.LayoutParameters = new ViewGroup.LayoutParams(200, 200);
            //    /* add Layout Parameters in just created view and set as the contentView of the activity */
            //    //sampleView.SetLayoutParams(new WindowManager.LayoutParams(WindowManager.LayoutParams.MATCH_PARENT, WindowManager.LayoutParams.MATCH_PARENT));
            //    SetContentView(sampleView);

            //}
            //else
            //{
            //    Log.Error("Json2View", "Could not load valid json file");
            //}

            // Set our view from the "main" layout resource
            //SetContentView(Resource.Layout.Main);
            //string path = Environment.ExternalStorageDirectory.Path;//.GetExternalStorageDirectory().getPath();
            //LinearLayout root = this.FindViewById<LinearLayout>(Resource.Id.root);

            //this.Resources.GetLayout()
            //XmlReader xmlReader = XmlReader.Create(path + "//button.xml");
            //LayoutInflater layoutInflater = LayoutInflater.From(this);
            //Android.Views.View view1 = layoutInflater.Inflate(Resource.Layout.button, root);
            //Android.Views.View view = layoutInflater.Inflate(xmlReader, null, false);
        }

        /**
     * Helper function to load file from assets
     */
        private string readFile(string fileName, Context context)
        {
            StringBuilder returnString = new StringBuilder();
            Stream fIn = null;
            InputStreamReader isr = null;
            BufferedReader input = null;
            try
            {
                fIn = context.Resources.Assets.Open(fileName);
                isr = new InputStreamReader(fIn);
                input = new BufferedReader(isr);
                string line;
                while ((line = input.ReadLine()) != null)
                {
                    returnString.Append(line);
                }
            }
            catch (System.Exception e)
            {
                string s = e.Message;
            }
            finally
            {
                try
                {
                    if (isr != null) isr.Close();
                    if (fIn != null) fIn.Close();
                    if (input != null) input.Close();
                }
                catch (System.Exception e2)
                {
                    string s = e2.Message;
                }
            }
            return returnString.ToString();
        }

    }


    ///**
    //     * Holder class that keep UI Component from the Dynamic View
    //     */
    //public class SampleViewHolder : Java.Lang.Object//,Java.Lang.Annotation.IAnnotation
    //{
    //    public View clickableView { get; set; }

    //    public SampleViewHolder()
    //    {
    //        DynamicViewId DynamicViewId = new DynamicViewId();
    //    }
    //}

}

