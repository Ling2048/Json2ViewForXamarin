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
using Org.Json;
using Java.Lang;
using Java.Lang.Reflect;
using Android.Text;

namespace Json2View
{
    public class DynamicView
    {
        static int mCurrentId = 13;
        static int INTERNAL_TAG_ID = 0x7f020000;

        /**
         * @param jsonObject : json object
         * @param holderClass : class that will be created as an holder and attached as a tag in the View
         * @return the view that created
         */
        public static View createView(Context context, JSONObject jsonObject, Class holderClass)
        {
            return createView(context, jsonObject, null, holderClass);
        }

        /**
         * @param jsonObject : json object
         * @param holderClass : class that will be created as an holder and attached as a tag in the View
         * @return the view that created
         */
        public static View createView(Context context, JSONObject jsonObject, Type holderClass)
        {
            return createView(context, jsonObject, null, holderClass);
        }

        /**
    * @param jsonObject : json object
    * @param parent : parent viewGroup
    * @param holderClass : class that will be created as an holder and attached as a tag in the View, If contains HashMap ids will replaced with idsMap
    * @return the view that created
    */
        public static View createView(Context context, JSONObject jsonObject, ViewGroup parent, Type holderClass)
        {

            if (jsonObject == null)
                return null;

            JavaDictionary<string, int> ids = new JavaDictionary<string, int>();

            View container = createViewInternal(context, jsonObject, parent, ids);

            if (container == null)
                return null;

            if (container.GetTag(INTERNAL_TAG_ID) != null)
                DynamicHelper.applyLayoutProperties(container, (JavaList<DynamicProperty>)container.GetTag(INTERNAL_TAG_ID), parent, ids);

            /* clear tag from properties */
            container.Tag = (INTERNAL_TAG_ID);

            if (holderClass != null)
            {

                try
                {
                    object holder = holderClass.Assembly.CreateInstance(holderClass.FullName);//.GetConstructor().NewInstance();
                    DynamicHelper.parseDynamicView(holder, container, ids);
                    container.Tag = (Java.Lang.Object)(holder);
                }
                catch (InstantiationException e)
                {
                    e.PrintStackTrace();
                }
                catch (IllegalAccessException e)
                {
                    e.PrintStackTrace();
                }
                catch (NoSuchMethodException e)
                {
                    e.PrintStackTrace();
                }
                catch (InvocationTargetException e)
                {
                    e.PrintStackTrace();
                }

            }

            return container;

        }

        /**
     * @param jsonObject : json object
     * @param parent : parent viewGroup
     * @param holderClass : class that will be created as an holder and attached as a tag in the View, If contains HashMap ids will replaced with idsMap
     * @return the view that created
     */
        public static View createView(Context context, JSONObject jsonObject, ViewGroup parent, Class holderClass)
        {

            if (jsonObject == null)
                return null;

            JavaDictionary<string,int> ids = new JavaDictionary<string, int>();

            View container = createViewInternal(context, jsonObject, parent, ids);

            if (container == null)
                return null;

            if (container.GetTag(INTERNAL_TAG_ID) != null)
                DynamicHelper.applyLayoutProperties(container, (JavaList<DynamicProperty>)container.GetTag(INTERNAL_TAG_ID), parent, ids);

            /* clear tag from properties */
            container.Tag = (INTERNAL_TAG_ID);

            if (holderClass != null)
            {

                try
                {
                    Java.Lang.Object holder = holderClass.GetConstructor().NewInstance();
                    DynamicHelper.parseDynamicView(holder, container, ids);
                    container.Tag = (holder);
                }
                catch (InstantiationException e)
                {
                    e.PrintStackTrace();
                }
                catch (IllegalAccessException e)
                {
                    e.PrintStackTrace();
                }
                catch (NoSuchMethodException e)
                {
                    e.PrintStackTrace();
                }
                catch (InvocationTargetException e)
                {
                    e.PrintStackTrace();
                }

            }

            return container;

        }

        /**
         * @param jsonObject : json object
         * @param parent : parent viewGroup
         * @return the view that created
         */
        //public static View createView(Context context, JSONObject jsonObject, ViewGroup parent)
        //{
        //    return createView(context, jsonObject, parent, null);
        //}

        ///**
        // * @param jsonObject : json object
        // * @return the view that created
        // */
        //public static View createView(Context context, JSONObject jsonObject)
        //{
        //    return createView(context, jsonObject, null, null);
        //}

        /**
     * use internal to parse the json as a tree to create View
     * @param jsonObject : json object
     * @param ids : the hashMap where we keep ids as string from json to ids as int in the layout
     * @return the view that created
     */
        private static View createViewInternal(Context context, JSONObject jsonObject, ViewGroup parent, JavaDictionary<string,int> ids)
        {

            View view = null;

            JavaList<DynamicProperty> properties;

            try
            {
                /* Create the View Object. If not full package is available try to create a view from android.widget */
                string widget = jsonObject.GetString("widget");
                if (!widget.Contains("."))
                {
                    widget = "android.widget." + widget;
                }
                Class viewClass = Class.ForName(widget);
                /* create the actual view object */
                view = (View)viewClass.GetConstructor(Java.Lang.Class.FromType(typeof(Context))).NewInstance(new Java.Lang.Object[] { context});
            }
            catch (JSONException e)
            {
                e.PrintStackTrace();
            }
            catch (ClassNotFoundException e)
            {
                e.PrintStackTrace();
            }
            catch (NoSuchMethodException e)
            {
                e.PrintStackTrace();
            }
            catch (InvocationTargetException e)
            {
                e.PrintStackTrace();
            }
            catch (InstantiationException e)
            {
                e.PrintStackTrace();
            }
            catch (IllegalAccessException e)
            {
                e.PrintStackTrace();
            }

            if (view==null) return null;

            try
            {

                /* default Layout in case the user not set it */
                ViewGroup.LayoutParams paramss = DynamicHelper.createLayoutParams(parent);
                view.LayoutParameters = (paramss);

                /* iterrate json and get all properties in array */
                properties = new JavaList<DynamicProperty>();
                JSONArray jArray = jsonObject.GetJSONArray("properties");
                if (jArray != null) {
                    for (int i=0;i<jArray.Length();i++){
                        DynamicProperty p = new DynamicProperty(jArray.GetJSONObject(i));
                        if (p.isValid())
                            properties.Add(p);
                    }
                }

                /* keep properties obj as a tag */
                view.SetTag(INTERNAL_TAG_ID, properties);

                /* add and integer as a universal id  and keep it in a hashmap */
                string id = DynamicHelper.applyStyleProperties(view, properties);
                if (!TextUtils.IsEmpty(id)) {
                    /* to target older versions we cannot use View.generateViewId();  */
                    ids.Add(id, mCurrentId);//.Put(id, mCurrentId);
                    view.Id = (mCurrentId );
                    mCurrentId++;
                }

                /* if view is type of ViewGroup check for its children view in json */
                if (view.GetType().BaseType == typeof(ViewGroup)) {
                    ViewGroup viewGroup = (ViewGroup)view;

                    /* parse the aray to get the children views */
                    JavaList<View> views = new JavaList<View>();
                    JSONArray jViews = jsonObject.OptJSONArray("views");
                    if (jViews != null) {
                        int count = jViews.Length();
                        for (int i=0;i<count;i++) {
                            /* create every child add it in viewGroup and set its tag with its properties */
                            View dynamicChildView = DynamicView.createViewInternal(context, jViews.GetJSONObject(i), parent, ids);
                            if (dynamicChildView!=null) {
                                views.Add(dynamicChildView);
                                viewGroup.AddView(dynamicChildView);
                            }
                        }
                    }
                    /* after create all the children apply layout properties
                    * we need to do this after al children creation to have create all possible ids */
                    foreach (View v in views) {
                        DynamicHelper.applyLayoutProperties(v, (JavaList<DynamicProperty>) v.GetTag(INTERNAL_TAG_ID), viewGroup, ids);
                        /* clear tag from properties */
                        v.SetTag(INTERNAL_TAG_ID, null);
                    }
                }

            }
            catch (JSONException e) {
                e.PrintStackTrace();
            }

            return view;

        }




    }
}