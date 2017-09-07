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
using Java.Lang;
using Java.IO;
using Android.Util;
using Org.Json;
using Android.Graphics.Drawables;
using Java.Util;
using Android.Text;
using Android.Graphics;
using Android.Content.Res;
using System.IO;

namespace Json2View
{
    public class DynamicProperty
    {

        /**
         * possible types that we handle
         **/
        public enum TYPE
        {
            NO_VALID,
            STRING,
            DIMEN,
            INTEGER,
            FLOAT,
            COLOR,
            REF,
            BOOLEAN,
            BASE64,
            DRAWABLE,
            JSON
        }

        /**
         * possible property name that we handle
         **/
        public enum NAME
        {
            NO_VALID,
            ID,
            LAYOUT_WIDTH,
            LAYOUT_HEIGHT,
            PADDING_LEFT,
            PADDING_RIGHT,
            PADDING_TOP,
            PADDING_BOTTOM,
            PADDING,
            LAYOUT_MARGINLEFT,
            LAYOUT_MARGINRIGHT,
            LAYOUT_MARGINTOP,
            LAYOUT_MARGINBOTTOM,
            LAYOUT_MARGIN,
            BACKGROUND,
            ENABLED,
            SELECTED,
            CLICKABLE,
            SCALEX,
            SCALEY,
            MINWIDTH,
            MINHEIGTH,
            VISIBILITY,
            /* textView */
            TEXT,
            TEXTCOLOR,
            TEXTSIZE,
            TEXTSTYLE,
            ELLIPSIZE,
            MAXLINES,
            GRAVITY,
            DRAWABLETOP,
            DRAWABLEBOTTOM,
            DRAWABLELEFT,
            DRAWABLERIGHT,
            /* imageView */
            SRC,
            SCALETYPE,
            ADJUSTVIEWBOUNDS,
            /* layout */
            LAYOUT_ABOVE,
            LAYOUT_ALIGNBASELINE,
            LAYOUT_ALIGNBOTTOM,
            LAYOUT_ALIGNEND,
            LAYOUT_ALIGNLEFT,
            LAYOUT_ALIGNPARENTBOTTOM,
            LAYOUT_ALIGNPARENTEND,
            LAYOUT_ALIGNPARENTLEFT,
            LAYOUT_ALIGNPARENTRIGHT,
            LAYOUT_ALIGNPARENTSTART,
            LAYOUT_ALIGNPARENTTOP,
            LAYOUT_ALIGNRIGHT,
            LAYOUT_ALIGNSTART,
            LAYOUT_ALIGNTOP,
            LAYOUT_ALIGNWITHPARENTIFMISSING,
            LAYOUT_BELOW,
            LAYOUT_CENTERHORIZONTAL,
            LAYOUT_CENTERINPARENT,
            LAYOUT_CENTERVERTICAL,
            LAYOUT_TOENDOF,
            LAYOUT_TOLEFTOF,
            LAYOUT_TORIGHTOF,
            LAYOUT_TOSTARTOF,
            LAYOUT_GRAVITY,
            LAYOUT_WEIGHT,
            SUM_WEIGHT,
            ORIENTATION,

            TAG,
            FUNCTION
        }

        public NAME name;
        public TYPE type;
        private Java.Lang.Object value;

        /**
         * @param v value to convert as string
         * @return Value as object depends on the type
         */
        private Java.Lang.Object convertValue(Java.Lang.Object v)
        {
            if (v == null)
                return null;
            switch (type)
            {
                case TYPE.INTEGER:
                    {
                        return Integer.ParseInt(v.ToString());
                    }
                case TYPE.FLOAT:
                    {
                        return Float.ParseFloat(v.ToString());
                    }
                case TYPE.DIMEN:
                    {
                        return convertDimenToPixel(v.ToString());
                    }
                case TYPE.COLOR:
                    {
                        return convertColor(v.ToString());
                    }
                case TYPE.BOOLEAN:
                    {
                        string value = v.ToString();
                        if (value.Equals("t"))//.equalsIgnoreCase("t"))
                        {
                            return true;
                        }
                        else if (value.Equals("f"))//equalsIgnoreCase("f"))
                        {
                            return false;
                        }
                        else if (value.Equals("true"))//equalsIgnoreCase("true"))
                        {
                            return true;
                        }
                        else if (value.Equals("false"))//equalsIgnoreCase("false"))
                        {
                            return false;
                        }
                        return Integer.ParseInt(value) == 1;
                    }
                case TYPE.BASE64:
                    {
                        try
                        {
                            //InputStream stream = new ByteArrayInputStream(Base64.Decode(v.ToString(), Base64.Default));
                            Stream stream = new MemoryStream(Base64.Decode(v.ToString(), Base64.Default));
                            return BitmapFactory.DecodeStream(stream);
                        }
                        catch (Java.Lang.Exception e)
                        {
                            return null;
                        }
                    }
                case TYPE.DRAWABLE:
                    {
                        JSONObject drawableProperties = (JSONObject)v;

                        GradientDrawable gd = new GradientDrawable();

                        if (drawableProperties != null)
                        {

                            try { gd.SetColor(convertColor(drawableProperties.GetString("COLOR"))); } catch (JSONException e) { }
                            if (drawableProperties.Has("CORNER"))
                            {
                                string cornerValues = null;
                                try
                                {
                                    cornerValues = drawableProperties.GetString("CORNER");
                                }
                                catch (JSONException e) { }
                                if (!TextUtils.IsEmpty(cornerValues))
                                {
                                    if (cornerValues.Contains("|"))
                                    {
                                        float[] corners = new float[8];
                                        Arrays.Fill(corners, 0);
                                        string[] values = cornerValues.Split('\\');//(.Split("\\|");
                                        int count = Java.Lang.Math.Min(values.Length, corners.Length);
                                        for (int i = 0; i < count; i++)
                                        {
                                            try
                                            {
                                                corners[i] = convertDimenToPixel(values[i]);
                                            }
                                            catch (Java.Lang.Exception e)
                                            {
                                                corners[i] = 0f;
                                            }
                                        }
                                        gd.SetCornerRadii(corners);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            gd.SetCornerRadius(convertDimenToPixel(cornerValues));
                                        }
                                        catch (Java.Lang.Exception e)
                                        {
                                            gd.SetCornerRadius(0f);
                                        }
                                    }
                                }

                            }
                            int strokeColor = 0x00FFFFFF;
                            int strokeSize = 0;
                            if (drawableProperties.Has("STROKECOLOR"))
                            {
                                try { strokeColor = convertColor(drawableProperties.GetString("STROKECOLOR")); } catch (JSONException e) { }
                            }
                            if (drawableProperties.Has("STROKESIZE"))
                            {
                                try { strokeSize = (int)convertDimenToPixel(drawableProperties.GetString("STROKESIZE")); } catch (JSONException e) { }
                            }
                            //gd.SetStroke(strokeSize, strokeColor);
                            gd.SetStroke(strokeSize, new Color(strokeColor));
                        }

                        return gd;
                    }
            }
            return v;
        }

        /**
         * create property and parse json
         * @param jsonObject : json to parse
         */
        public DynamicProperty(JSONObject jsonObject) : base()
        {
            try
            {
                //name = NAME.ValueOf(jsonObject.GetString("name").ToUpper().Trim());//toUpperCase().trim());
                System.Enum.TryParse<NAME>(jsonObject.GetString("name").ToUpper().Trim(),out name); // NAME.ValueOf(jsonObject.GetString("name").ToUpper().Trim());
            }
            catch (Java.Lang.Exception e)
            {
                name = NAME.NO_VALID;
            }
            try
            {
                //type = TYPE.valueOf(jsonObject.GetString("type").ToUpper().Trim());//.ToUpperCase().trim());
                System.Enum.TryParse<TYPE>(jsonObject.GetString("type").ToUpper().Trim(), out type);
            }
            catch (Java.Lang.Exception e)
            {
                type = TYPE.NO_VALID;
            }
            try
            {
                value = convertValue(jsonObject.Get("value"));
            }
            catch (Java.Lang.Exception e) { }
        }

        public bool isValid()
        {
            return value != null;
        }

        /**
         * @param clazz
         * @param varName
         * @return search in clazz of possible variable name (varName) and return its value
         */
        public Java.Lang.Object getValueInt(Class clazz, string varName)
        {

            Java.Lang.Reflect.Field fieldRequested = null;

            try
            {
                fieldRequested = clazz.GetField(varName);
                if (fieldRequested != null)
                {
                    return fieldRequested.Get(clazz);
                }
            }
            catch (SecurityException e)
            {
                e.PrintStackTrace();
            }
            catch (NoSuchFieldException e)
            {
                e.PrintStackTrace();
            }
            catch (IllegalAccessException e)
            {
                e.PrintStackTrace();
            }
            catch (IllegalArgumentException e)
            {
                e.PrintStackTrace();
            }
            return null;
        }


        /** next function just cast value and return the object **/
        //public int Color { get
        //    {
        //        if (type == TYPE.COLOR)
        //            return Convert.ToInt32(value);
        //        //return Integer.class.cast(value);
        //        return -1;
        //    }
        //}

        public int getValueColor()
        {
            if (type == TYPE.COLOR)
                return Convert.ToInt32(value);
            //return Integer.class.cast(value);
            return -1;
        }
        public string getValueString()
        {
            return value.ToString();
            //return string.class.cast(value);
        }
        public int getValueInt()
        {
            if (value.GetType() == typeof(int))
                return Convert.ToInt32(value);
            //return Integer.class.cast(value);
            else if (value.GetType() == typeof(float))
                return (int) getValueFloat();
            else
            return (int) value;
        }
        public float getValueFloat()
        {
            return Convert.ToSingle(value);// Float.class.cast(value);
        }
        public bool getValueBoolean()
        {
            return Convert.ToBoolean(value);// Boolean.class.cast(value);
        }
        public Bitmap getValueBitmap()
        {
            return (Bitmap)value;
        }
        public Drawable getValueBitmapDrawable()
        {
            return new BitmapDrawable(Resources.System, getValueBitmap());
        }
        public Drawable getValueGradientDrawable()
        {
            return (Drawable)value;
        }
        public JSONObject getValueJSON()
        {
            return (JSONObject)value;
        }


        int convertColor(string color)
        {
            if (color.StartsWith("0x"))
            {
                return (int)Long.ParseLong(color.Substring(2), 16);
            }
            return Color.ParseColor(color);
        }

        float convertDimenToPixel(string dimen)
        {
            if (dimen.EndsWith("dp"))
                return DynamicHelper.dpToPx(Float.ParseFloat(dimen.Substring(0, dimen.Length - 2)));
            else if (dimen.EndsWith("sp"))
                return DynamicHelper.spToPx(Float.ParseFloat(dimen.Substring(0, dimen.Length - 2)));
            else if (dimen.EndsWith("px"))
                return Integer.ParseInt(dimen.Substring(0, dimen.Length - 2));
            else if (dimen.EndsWith("%"))
                return (int)(Float.ParseFloat(dimen.Substring(0, dimen.Length - 1)) / 100f * DynamicHelper.deviceWidth());
            else if (dimen.Equals("match_parent"))//equalsIgnoreCase("match_parent"))
                return ViewGroup.LayoutParams.MatchParent;//.MATCH_PARENT;
            else if (dimen.Equals("wrap_content"))//.EqualsIgnoreCase("wrap_content"))
                return ViewGroup.LayoutParams.WrapContent;
            else
                return Integer.ParseInt(dimen);
        }
    }
}