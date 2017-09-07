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
using static Json2View.DynamicProperty;
using Java.Lang;
using Android.Util;
using Android.Text;
using Android.Graphics.Drawables;
using Org.Json;
using Android.Content.Res;

namespace Json2View
{
    public class DynamicHelper
    {
        /**
     * apply dynamic properties that are not relative with layout in view
     *
     * @param view
     * @param properties
     */
        public static string applyStyleProperties(View view, JavaList<DynamicProperty> properties)
        {
            string id = "";
            foreach (DynamicProperty dynProp in properties)
            {
                switch (dynProp.name)
                {
                    case NAME.ID:
                        {
                            id = dynProp.getValueString();
                        }
                        break;
                    case NAME.BACKGROUND:
                        {
                            applyBackground(view, dynProp);
                        }
                        break;
                    case NAME.TEXT:
                        {
                            applyText(view, dynProp);
                        }
                        break;
                    case NAME.TEXTCOLOR:
                        {
                            applyTextColor(view, dynProp);
                        }
                        break;
                    case NAME.TEXTSIZE:
                        {
                            applyTextSize(view, dynProp);
                        }
                        break;
                    case NAME.TEXTSTYLE:
                        {
                            applyTextStyle(view, dynProp);
                        }
                        break;
                    case NAME.PADDING:
                        {
                            applyPadding(view, dynProp);
                        }
                        break;
                    case NAME.PADDING_LEFT:
                        {
                            applyPadding(view, dynProp, 0);
                        }
                        break;
                    case NAME.PADDING_TOP:
                        {
                            applyPadding(view, dynProp, 1);
                        }
                        break;
                    case NAME.PADDING_RIGHT:
                        {
                            applyPadding(view, dynProp, 2);
                        }
                        break;
                    case NAME.PADDING_BOTTOM:
                        {
                            applyPadding(view, dynProp, 3);
                        }
                        break;
                    case NAME.MINWIDTH:
                        {
                            applyMinWidth(view, dynProp);
                        }
                        break;
                    case NAME.MINHEIGTH:
                        {
                            applyMinHeight(view, dynProp);
                        }
                        break;
                    case NAME.ELLIPSIZE:
                        {
                            applyEllipsize(view, dynProp);
                        }
                        break;
                    case NAME.MAXLINES:
                        {
                            applyMaxLines(view, dynProp);
                        }
                        break;
                    case NAME.ORIENTATION:
                        {
                            applyOrientation(view, dynProp);
                        }
                        break;
                    case NAME.SUM_WEIGHT:
                        {
                            applyWeightSum(view, dynProp);
                        }
                        break;
                    case NAME.GRAVITY:
                        {
                            applyGravity(view, dynProp);
                        }
                        break;
                    case NAME.SRC:
                        {
                            applySrc(view, dynProp);
                        }
                        break;
                    case NAME.SCALETYPE:
                        {
                            applyScaleType(view, dynProp);
                        }
                        break;
                    case NAME.ADJUSTVIEWBOUNDS:
                        {
                            applyAdjustBounds(view, dynProp);
                        }
                        break;
                    case NAME.DRAWABLELEFT:
                        {
                            applyCompoundDrawable(view, dynProp, 0);
                        }
                        break;
                    case NAME.DRAWABLETOP:
                        {
                            applyCompoundDrawable(view, dynProp, 1);
                        }
                        break;
                    case NAME.DRAWABLERIGHT:
                        {
                            applyCompoundDrawable(view, dynProp, 2);
                        }
                        break;
                    case NAME.DRAWABLEBOTTOM:
                        {
                            applyCompoundDrawable(view, dynProp, 3);
                        }
                        break;
                    case NAME.ENABLED:
                        {
                            applyEnabled(view, dynProp);
                        }
                        break;
                    case NAME.SELECTED:
                        {
                            applySelected(view, dynProp);
                        }
                        break;
                    case NAME.CLICKABLE:
                        {
                            applyClickable(view, dynProp);
                        }
                        break;
                    case NAME.SCALEX:
                        {
                            applyScaleX(view, dynProp);
                        }
                        break;
                    case NAME.SCALEY:
                        {
                            applyScaleY(view, dynProp);
                        }
                        break;
                    case NAME.TAG:
                        {
                            applyTag(view, dynProp);
                        }
                        break;
                    case NAME.FUNCTION:
                        {
                            applyFunction(view, dynProp);
                        }
                        break;
                    case NAME.VISIBILITY:
                        {
                            applyVisibility(view, dynProp);
                        }
                        break;
                }
            }
            return id;
        }

        /**
     * apply dynamic properties for layout in view
     *
     * @param view
     * @param properties : layout properties to apply
     * @param viewGroup  : parent view
     * @param ids        : hashmap of ids <String, Integer> (string as setted in json, int that we use in layout)
     */
        public static void applyLayoutProperties(View view, JavaList<DynamicProperty> properties, ViewGroup viewGroup, JavaDictionary<string, int> ids)
        {
            if (viewGroup == null)
                return;
            ViewGroup.LayoutParams paramss = createLayoutParams(viewGroup);

            foreach (DynamicProperty dynProp in properties)
            {
                try
                {
                    switch (dynProp.name)
                    {
                        case NAME.LAYOUT_HEIGHT:
                            {
                                paramss.Height = dynProp.getValueInt();
                            }
                            break;
                        case NAME.LAYOUT_WIDTH:
                            {
                                paramss.Width = dynProp.getValueInt();
                            }
                            break;
                        case NAME.LAYOUT_MARGIN:
                            {
                                if (paramss.GetType() == typeof(ViewGroup.MarginLayoutParams))
                                {
                                    ViewGroup.MarginLayoutParams p = ((ViewGroup.MarginLayoutParams)paramss);
                                    p.BottomMargin = p.TopMargin = p.LeftMargin = p.RightMargin = dynProp.getValueInt();
                                }
                            }
                            break;
                        case NAME.LAYOUT_MARGINLEFT:
                            {
                                if (paramss.GetType() == typeof(ViewGroup.MarginLayoutParams))
                                {
                                    ((ViewGroup.MarginLayoutParams)paramss).LeftMargin = dynProp.getValueInt();
                                }
                            }
                            break;
                        case NAME.LAYOUT_MARGINTOP:
                            {
                                if (paramss.GetType() == typeof(ViewGroup.MarginLayoutParams))
                                {
                                    ((ViewGroup.MarginLayoutParams)paramss).TopMargin = dynProp.getValueInt();
                                }
                            }
                            break;
                        case NAME.LAYOUT_MARGINRIGHT:
                            {
                                if (paramss.GetType() == typeof(ViewGroup.MarginLayoutParams))
                                {
                                    ((ViewGroup.MarginLayoutParams)paramss).RightMargin = dynProp.getValueInt();
                                }
                            }
                            break;
                        case NAME.LAYOUT_MARGINBOTTOM:
                            {
                                if (paramss.GetType() == typeof(ViewGroup.MarginLayoutParams))
                                {
                                    ((ViewGroup.MarginLayoutParams)paramss).BottomMargin = dynProp.getValueInt();
                                }
                            }
                            break;
                        case NAME.LAYOUT_ABOVE:
                            {
                                if (paramss.GetType() == typeof(RelativeLayout.LayoutParams))
                                    ((RelativeLayout.LayoutParams)paramss).AddRule(LayoutRules.Above, ids[dynProp.getValueString()]);
                            }
                            break;
                        case NAME.LAYOUT_BELOW:
                            {
                                if (paramss.GetType() == typeof(RelativeLayout.LayoutParams))
                                    ((RelativeLayout.LayoutParams)paramss).AddRule(LayoutRules.Below, ids[dynProp.getValueString()]);
                            }
                            break;
                        case NAME.LAYOUT_TOLEFTOF:
                            {
                                if (paramss.GetType() == typeof(RelativeLayout.LayoutParams))
                                    ((RelativeLayout.LayoutParams)paramss).AddRule(LayoutRules.LeftOf, ids[dynProp.getValueString()]);
                            }
                            break;
                        case NAME.LAYOUT_TORIGHTOF:
                            {
                                if (paramss.GetType() == typeof(RelativeLayout.LayoutParams))
                                    ((RelativeLayout.LayoutParams)paramss).AddRule(LayoutRules.RightOf, ids[dynProp.getValueString()]);
                            }
                            break;
                        case NAME.LAYOUT_TOSTARTOF:
                            {
                                if (paramss.GetType() == typeof(RelativeLayout.LayoutParams))
                                    ((RelativeLayout.LayoutParams)paramss).AddRule(LayoutRules.StartOf, ids[dynProp.getValueString()]);
                            }
                            break;
                        case NAME.LAYOUT_TOENDOF:
                            {
                                if (paramss.GetType() == typeof(RelativeLayout.LayoutParams))
                                    ((RelativeLayout.LayoutParams)paramss).AddRule(LayoutRules.EndOf, ids[dynProp.getValueString()]);
                            }
                            break;
                        case NAME.LAYOUT_ALIGNBASELINE:
                            {
                                if (paramss.GetType() == typeof(RelativeLayout.LayoutParams))
                                    ((RelativeLayout.LayoutParams)paramss).AddRule(LayoutRules.AlignBaseline, ids[dynProp.getValueString()]);
                            }
                            break;
                        case NAME.LAYOUT_ALIGNLEFT:
                            {
                                if (paramss.GetType() == typeof(RelativeLayout.LayoutParams))
                                    ((RelativeLayout.LayoutParams)paramss).AddRule(LayoutRules.AlignLeft, ids[dynProp.getValueString()]);
                            }
                            break;
                        case NAME.LAYOUT_ALIGNTOP:
                            {
                                if (paramss.GetType() == typeof(RelativeLayout.LayoutParams))
                                    ((RelativeLayout.LayoutParams)paramss).AddRule(LayoutRules.AlignTop, ids[dynProp.getValueString()]);
                            }
                            break;
                        case NAME.LAYOUT_ALIGNRIGHT:
                            {
                                if (paramss.GetType() == typeof(RelativeLayout.LayoutParams))
                                    ((RelativeLayout.LayoutParams)paramss).AddRule(LayoutRules.AlignRight, ids[dynProp.getValueString()]);
                            }
                            break;
                        case NAME.LAYOUT_ALIGNBOTTOM:
                            {
                                if (paramss.GetType() == typeof(RelativeLayout.LayoutParams))
                                    ((RelativeLayout.LayoutParams)paramss).AddRule(LayoutRules.AlignBottom, ids[dynProp.getValueString()]);
                            }
                            break;
                        case NAME.LAYOUT_ALIGNSTART:
                            {
                                if (paramss.GetType() == typeof(RelativeLayout.LayoutParams))
                                    ((RelativeLayout.LayoutParams)paramss).AddRule(LayoutRules.AlignStart, ids[dynProp.getValueString()]);
                            }
                            break;
                        case NAME.LAYOUT_ALIGNEND:
                            {
                                if (paramss.GetType() == typeof(RelativeLayout.LayoutParams))
                                    ((RelativeLayout.LayoutParams)paramss).AddRule(LayoutRules.AlignEnd, ids[dynProp.getValueString()]);
                            }
                            break;
                        case NAME.LAYOUT_ALIGNWITHPARENTIFMISSING:
                            {
                                if (paramss.GetType() == typeof(RelativeLayout.LayoutParams))
                                    ((RelativeLayout.LayoutParams)paramss).AlignWithParent = dynProp.getValueBoolean();
                            }
                            break;
                        case NAME.LAYOUT_ALIGNPARENTTOP:
                            {
                                if (paramss.GetType() == typeof(RelativeLayout.LayoutParams))
                                    ((RelativeLayout.LayoutParams)paramss).AddRule(LayoutRules.AlignParentTop);
                            }
                            break;
                        case NAME.LAYOUT_ALIGNPARENTBOTTOM:
                            {
                                if (paramss.GetType() == typeof(RelativeLayout.LayoutParams))
                                    ((RelativeLayout.LayoutParams)paramss).AddRule(LayoutRules.AlignParentBottom);
                            }
                            break;
                        case NAME.LAYOUT_ALIGNPARENTLEFT:
                            {
                                if (paramss.GetType() == typeof(RelativeLayout.LayoutParams))
                                    ((RelativeLayout.LayoutParams)paramss).AddRule(LayoutRules.AlignParentLeft);
                            }
                            break;
                        case NAME.LAYOUT_ALIGNPARENTRIGHT:
                            {
                                if (paramss.GetType() == typeof(RelativeLayout.LayoutParams))
                                    ((RelativeLayout.LayoutParams)paramss).AddRule(LayoutRules.AlignParentRight);
                            }
                            break;
                        case NAME.LAYOUT_ALIGNPARENTSTART:
                            {
                                if (paramss.GetType() == typeof(RelativeLayout.LayoutParams))
                                    ((RelativeLayout.LayoutParams)paramss).AddRule(LayoutRules.AlignParentStart);
                            }
                            break;
                        case NAME.LAYOUT_ALIGNPARENTEND:
                            {
                                if (paramss.GetType() == typeof(RelativeLayout.LayoutParams))
                                    ((RelativeLayout.LayoutParams)paramss).AddRule(LayoutRules.AlignParentEnd);
                            }
                            break;
                        case NAME.LAYOUT_CENTERHORIZONTAL:
                            {
                                if (paramss.GetType() == typeof(RelativeLayout.LayoutParams))
                                    ((RelativeLayout.LayoutParams)paramss).AddRule(LayoutRules.CenterHorizontal);
                            }
                            break;
                        case NAME.LAYOUT_CENTERVERTICAL:
                            {
                                if (paramss.GetType() == typeof(RelativeLayout.LayoutParams))
                                    ((RelativeLayout.LayoutParams)paramss).AddRule(LayoutRules.CenterVertical);
                            }
                            break;
                        case NAME.LAYOUT_CENTERINPARENT:
                            {
                                if (paramss.GetType() == typeof(RelativeLayout.LayoutParams))
                                    ((RelativeLayout.LayoutParams)paramss).AddRule(LayoutRules.CenterInParent);
                            }
                            break;
                        case NAME.LAYOUT_GRAVITY:
                            {
                                switch (dynProp.type)
                                {
                                    case TYPE.INTEGER:
                                        {
                                            if (paramss.GetType() == typeof(LinearLayout.LayoutParams))
                                            {
                                                GravityFlags gravityFlags;
                                                System.Enum.TryParse<GravityFlags>(dynProp.getValueString(), out gravityFlags);
                                                ((LinearLayout.LayoutParams)paramss).Gravity = gravityFlags;
                                            }
                                        }
                                        break;
                                    case TYPE.STRING:
                                        {
                                            if (paramss.GetType() == typeof(LinearLayout.LayoutParams))
                                            {
                                                GravityFlags gravityFlags;
                                                System.Enum.TryParse<GravityFlags>(dynProp.getValueInt(Java.Lang.Class.FromType(typeof(Gravity)), dynProp.getValueString().ToUpper()).ToString(), out gravityFlags);
                                                ((LinearLayout.LayoutParams)paramss).Gravity = gravityFlags;
                                            }
                                            //((LinearLayout.LayoutParams)paramss).Gravity = (int)dynProp.getValueInt(Java.Lang.Class.FromType(typeof(Gravity)), dynProp.getValueString().ToUpper());
                                        }
                                        break;
                                }
                            }
                            break;
                        case NAME.LAYOUT_WEIGHT:
                            {
                                switch (dynProp.type)
                                {
                                    case TYPE.FLOAT:
                                        {
                                            if (paramss.GetType() == typeof(LinearLayout.LayoutParams))
                                                ((LinearLayout.LayoutParams)paramss).Weight = dynProp.getValueFloat();
                                        }
                                        break;
                                }
                            }
                            break;
                    }
                }
                catch (Java.Lang.Exception e)
                {
                }
            }
        }




        public static ViewGroup.LayoutParams createLayoutParams(ViewGroup viewGroup)
        {
            ViewGroup.LayoutParams paramss = null;
            if (viewGroup != null)
            {
                try
                {
                    /* find parent viewGroup and create LayoutParams of that class */
                    Class layoutClass = viewGroup.Class;//.getClass();
                    while (!classExists(layoutClass.Name + "$LayoutParams"))
                    {
                        layoutClass = layoutClass.Superclass;//.GetSuperclass();
                    }
                    string layoutParamsClassname = layoutClass.Name + "$LayoutParams";
                    Class layoutParamsClass = Class.ForName(layoutParamsClassname);
                    /* create the actual layoutParams object */
                    paramss = (ViewGroup.LayoutParams)layoutParamsClass.GetConstructor(Integer.Type, Integer.Type).NewInstance(new Java.Lang.Object[] { ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent });
                }
                catch (Java.Lang.Exception e)
                {
                    e.PrintStackTrace();
                }
            }
            if (paramss == null)
                paramss = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);

            return paramss;
        }

        /**
     * apply background in view. possible type :
     * - COLOR
     * - REF => search for that drawable in resources
     * - BASE64 => convert base64 to bitmap and apply in view
     */
        public static void applyBackground(View view, DynamicProperty property)
        {
            if (view != null)
            {
                switch (property.type)
                {
                    case TYPE.COLOR:
                        {
                            view.SetBackgroundColor(new Android.Graphics.Color(property.getValueColor()));
                        }
                        break;
                    case TYPE.REF:
                        {
                            view.SetBackgroundResource(getDrawableId(view.Context, property.getValueString()));
                        }
                        break;
                    case TYPE.BASE64:
                        {
                            if (Android.OS.Build.VERSION.PreviewSdkInt >= (int)Android.OS.BuildVersionCodes.JellyBean)
                                view.Background = property.getValueBitmapDrawable();//.SetBackground();
                            else
                                view.SetBackgroundDrawable(property.getValueBitmapDrawable());
                        }
                        break;
                    case TYPE.DRAWABLE:
                        {
                            if (Android.OS.Build.VERSION.PreviewSdkInt >= (int)Android.OS.BuildVersionCodes.JellyBean)
                                view.Background = (property.getValueGradientDrawable());
                            else
                                view.SetBackgroundDrawable(property.getValueBitmapDrawable());
                            //view.setBackgroundDrawable(property.getValueGradientDrawable());
                        }
                        break;
                }
            }
        }



        /**
         * apply padding in view
         */
        public static void applyPadding(View view, DynamicProperty property)
        {
            if (view != null)
            {
                switch (property.type)
                {
                    case TYPE.DIMEN:
                        {
                            int padding = property.getValueInt();
                            view.SetPadding(padding, padding, padding, padding);
                        }
                        break;
                }
            }
        }

        /**
         * apply padding in view
         */
        public static void applyPadding(View view, DynamicProperty property, int position)
        {
            if (view != null)
            {
                switch (property.type)
                {
                    case TYPE.DIMEN:
                        {
                            int[] padding = new int[] 
                            {
                              view.PaddingLeft,//.GetPaddingLeft(),
                              view.PaddingTop,//.getPaddingTop(),
                              view.PaddingRight,//.getPaddingRight(),
                              view.PaddingBottom//.getPaddingBottom()
                            };
                            padding[position] = property.getValueInt();
                            view.SetPadding(padding[0], padding[1], padding[2], padding[3]);
                        }
                        break;
                }
            }
        }

        /**
         * apply minimum Width in view
         */
        public static void applyMinWidth(View view, DynamicProperty property)
        {
            if (view != null)
            {
                if (property.type == DynamicProperty.TYPE.DIMEN)
                {
                    view.SetMinimumWidth(property.getValueInt());
                }
            }
        }

        /**
         * apply minimum Height in view
         */
        public static void applyMinHeight(View view, DynamicProperty property)
        {
            if (view != null)
            {
                if (property.type == DynamicProperty.TYPE.DIMEN)
                {
                    view.SetMinimumHeight(property.getValueInt());
                }
            }
        }

        /**
         * apply enabled in view
         */
        public static void applyEnabled(View view, DynamicProperty property)
        {
            if (view != null)
            {
                switch (property.type)
                {
                    case TYPE.BOOLEAN:
                        {
                            view.Enabled = (property.getValueBoolean());
                        }
                        break;
                }
            }
        }

        /**
         * apply selected in view
         */
        public static void applySelected(View view, DynamicProperty property)
        {
            if (view != null)
            {
                switch (property.type)
                {
                    case TYPE.BOOLEAN:
                        {
                            view.Selected = property.getValueBoolean();//.SetSelected(property.getValueBoolean());
                        }
                        break;
                }
            }
        }

        /**
         * apply clickable in view
         */
        public static void applyClickable(View view, DynamicProperty property)
        {
            if (view != null)
            {
                switch (property.type)
                {
                    case TYPE.BOOLEAN:
                        {
                            view.Clickable = property.getValueBoolean();//.SetClickable(property.getValueBoolean());
                        }
                        break;
                }
            }
        }

        /**
         * apply selected in view
         */
        public static void applyScaleX(View view, DynamicProperty property)
        {
            if (view != null)
            {
                switch (property.type)
                {
                    case TYPE.BOOLEAN:
                        {
                            view.ScaleX = property.getValueFloat();//.SetScaleX(property.getValueFloat());
                        }
                        break;
                }
            }
        }

        /**
         * apply selected in view
         */
        public static void applyScaleY(View view, DynamicProperty property)
        {
            if (view != null)
            {
                switch (property.type)
                {
                    case TYPE.BOOLEAN:
                        {
                            view.ScaleY = property.getValueFloat();//.setScaleY(property.getValueFloat());
                        }
                        break;
                }
            }
        }

        /**
         *  apply visibility in view
         */
        private static void applyVisibility(View view, DynamicProperty property)
        {
            if (view != null)
            {
                switch (property.type)
                {
                    case TYPE.STRING:
                        {
                            switch (property.getValueString())
                            {
                                case "gone":
                                    {
                                        view.Visibility = ViewStates.Gone;//.SetVisibility(View.GONE);
                                    }
                                    break;
                                case "visible":
                                    {
                                        view.Visibility = ViewStates.Visible;//.setVisibility(View.VISIBLE);
                                    }
                                    break;
                                case "invisible":
                                    {
                                        view.Visibility = ViewStates.Invisible;//.setVisibility(View.INVISIBLE);
                                    }
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        /*** TextView Properties ***/

        /**
         * apply text (used only in TextView)
         * - STRING : the actual string to set in textView
         * - REF : the name of string resource to apply in textView
         */
        public static void applyText(View view, DynamicProperty property)
        {
            if (view.GetType() == typeof(TextView))
            {
                switch (property.type)
                {
                    case TYPE.STRING:
                        {
                            ((TextView)view).Text = (property.getValueString());
                        }
                        break;
                    case TYPE.REF:
                        {
                            ((TextView)view).SetText(getStringId(view.Context, property.getValueString()));// Text = ();
                        }
                        break;
                }
            }
        }

        /**
         * apply the color in textView
         */
        public static void applyTextColor(View view, DynamicProperty property)
        {
            if (view.GetType() == typeof(TextView)) {
                switch (property.type)
                {
                    case TYPE.COLOR:
                        {
                            ((TextView)view).SetTextColor(new Android.Graphics.Color(property.getValueColor()));// = ();
                        }
                        break;
                }
            }
        }

        /**
         * apply the textSize in textView
         */
        public static void applyTextSize(View view, DynamicProperty property)
        {
            if (view.GetType() == typeof(TextView)) {
                switch (property.type)
                {
                    case TYPE.DIMEN:
                        {
                            ((TextView)view).SetTextSize(ComplexUnitType.Px, property.getValueFloat());// = (TypedValue.com.COMPLEX_UNIT_PX, property.getValueFloat());
                        }
                        break;
                }
            }
        }

        /**
         * apply the textStyle in textView
         */
        public static void applyTextStyle(View view, DynamicProperty property)
        {
            if (view.GetType() == typeof(TextView)) {
                switch (property.type)
                {
                    case TYPE.INTEGER:
                        {
                            Android.Graphics.TypefaceStyle typefaceStyle;
                            System.Enum.TryParse<Android.Graphics.TypefaceStyle>(property.getValueString(), out typefaceStyle);
                            ((TextView)view).SetTypeface(null, typefaceStyle);
                        }
                        break;
                }
            }
        }

        /**
        * apply ellipsize property in textView
        */
        public static void applyEllipsize(View view, DynamicProperty property)
        {
            if (view.GetType() == typeof(TextView)) {
                ((TextView)view).Ellipsize = (TextUtils.TruncateAt.ValueOf(property.getValueString().ToUpper().Trim()));
            }
        }

        /**
         * apply maxLines property in textView
         */
        public static void applyMaxLines(View view, DynamicProperty property)
        {
            if (view.GetType() == typeof(TextView)) {
                ((TextView)view).SetMaxLines(property.getValueInt());
            }
        }

        /**
         * apply gravity property in textView
         * - INTEGER => valus of gravity in @link(Gravity.java)
         * - STRING => name of variable in @lin(Gravity.java)
         */
        public static void applyGravity(View view, DynamicProperty property)
        {
            if (view.GetType() == typeof(TextView))
            {
                switch (property.type)
                {
                    case TYPE.INTEGER:
                        {
                            GravityFlags gravityFlags;
                            System.Enum.TryParse<GravityFlags>(property.getValueString(), out gravityFlags);
                            ((TextView)view).Gravity = gravityFlags;
                        }
                        break;
                    case TYPE.STRING:
                        {
                            GravityFlags gravityFlags;
                            System.Enum.TryParse<GravityFlags>(((Integer)property.getValueInt(Java.Lang.Class.FromType(typeof(Gravity)), property.getValueString().ToUpper())).ToString(), out gravityFlags);
                            ((TextView)view).Gravity = gravityFlags;
                        }
                        break;
                }
            }
        }

        /**
         * apply compound property in textView
         * position 0:left, 1:top, 2:right, 3:bottom
         * - REF : drawable to load as compoundDrawable
         * - BASE64 : decode as base64 and set as CompoundDrawable
         */
        public static void applyCompoundDrawable(View view, DynamicProperty property, int position)
        {
            if (view.GetType() == typeof(TextView)) {
                TextView textView = (TextView)view;
                Drawable[] d = textView.GetCompoundDrawables();//.getCompoundDrawables();
                switch (property.type)
                {
                    case TYPE.REF:
                        {
                            try
                            {
                                d[position] = view.Context.Resources.GetDrawable(getDrawableId(view.Context, property.getValueString()));//.getResources().getDrawable();
                            }
                            catch (Java.Lang.Exception e) { }
                        }
                        break;
                    case TYPE.BASE64:
                        {
                            d[position] = property.getValueBitmapDrawable();
                        }
                        break;
                    case TYPE.DRAWABLE:
                        {
                            d[position] = property.getValueGradientDrawable();
                        }
                        break;
                }
                textView.SetCompoundDrawablesWithIntrinsicBounds(d[0], d[1], d[2], d[3]);
            }
        }

        /*** ImageView Properties ***/

        /**
         * apply src property in imageView
         * - REF => name of drawable
         * - BASE64 => decode value as base64 image
         */
        public static void applySrc(View view, DynamicProperty property)
        {
            if (view.GetType() == typeof(TextView)) {
                switch (property.type)
                {
                    case TYPE.REF:
                        {
                            ((ImageView)view).SetImageResource(getDrawableId(view.Context, property.getValueString()));
                        }
                        break;
                    case TYPE.BASE64:
                        {
                            ((ImageView)view).SetImageBitmap(property.getValueBitmap());
                        }
                        break;
                }
            }
        }

        /**
         * apply scaleType property in ImageView
         */
        public static void applyScaleType(View view, DynamicProperty property)
        {
            if (view.GetType() == typeof(TextView)) {
                switch (property.type)
                {
                    case TYPE.STRING:
                        {
                            ((ImageView)view).SetScaleType(ImageView.ScaleType.ValueOf(property.getValueString().ToUpper()));
                        }
                        break;
                }
            }
        }

        /**
         * apply adjustBounds property in ImageView
         */
        public static void applyAdjustBounds(View view, DynamicProperty property)
        {
            if (view.GetType() == typeof(TextView)) {
                switch (property.type)
                {
                    case TYPE.BOOLEAN:
                        {
                            ((ImageView)view).SetAdjustViewBounds(property.getValueBoolean());
                        }
                        break;
                }
            }
        }

        /*** LinearLayout Properties ***/

        /**
         * apply orientation property in LinearLayout
         * - INTEGER => 0:Horizontal , 1:Vertical
         * - STRING
         */
        public static void applyOrientation(View view, DynamicProperty property)
        {
            if (view.GetType() == typeof(TextView)) {
                switch (property.type)
                {
                    case TYPE.INTEGER:
                        {
                            ((LinearLayout)view).Orientation = (property.getValueInt() == 0 ? Android.Widget.Orientation.Horizontal : Android.Widget.Orientation.Vertical);
                        }
                        break;
                    case TYPE.STRING:
                        {
                            ((LinearLayout)view).Orientation = (property.getValueString().Equals("HORIZONTAL") ? Android.Widget.Orientation.Horizontal : Android.Widget.Orientation.Vertical);
                        }
                        break;
                }
            }
        }

        /**
         * apply WeightSum property in LinearLayout
         */
        public static void applyWeightSum(View view, DynamicProperty property)
        {
            if ((view.GetType() == typeof(TextView)) && (property.type == DynamicProperty.TYPE.FLOAT)) {
                ((LinearLayout)view).WeightSum = (property.getValueFloat());
            }
        }

        /**
         * add string as tag
         */
        public static void applyTag(View view, DynamicProperty property)
        {
            view.Tag = (property.getValueString());
        }

        /**
     * apply generic function in View
     */
        public static void applyFunction(View view, DynamicProperty property)
        {

            if (property.type == DynamicProperty.TYPE.JSON)
            {
                try
                {
                    JSONObject json = property.getValueJSON();

                    string functionName = json.GetString("function");
                    JSONArray args = json.GetJSONArray("args");

                    Class[] argsClass;
                    Java.Lang.Object[] argsValue;
                    if (args == null)
                    {
                        argsClass = new Class[0];
                        argsValue = new Java.Lang.Object[0];
                    }
                    else
                    {
                        try
                        {
                            List<Class> classList = new List<Class>();// ArrayList<>();
                            List<Java.Lang.Object> valueList = new List<Java.Lang.Object>();// ArrayList<>();

                            int i = 0;
                            int count = args.Length();
                            for (; i < count; i++)
                            {
                                JSONObject argJsonObj = args.GetJSONObject(i);
                                bool isPrimitive = argJsonObj.Has("primitive");
                                string className = argJsonObj.GetString(isPrimitive ? "primitive" : "class");
                                string classFullName = className;
                                if (!classFullName.Contains("."))
                                    classFullName = "java.lang." + className;
                                Class clazz = Class.ForName(classFullName);
                                if (isPrimitive)
                                {
                                    Class primitiveType = (Class)clazz.GetField("TYPE").Get(null);
                                    classList.Add(primitiveType);
                                }
                                else
                                {
                                    classList.Add(clazz);
                                }

                                try
                                {
                                    valueList.Add(getFromJSON(argJsonObj, "value", clazz));
                                }
                                catch (Java.Lang.Exception e)
                                {
                                    e.PrintStackTrace();
                                }
                            }
                            argsClass = classList.ToArray();
                            argsValue = valueList.ToArray();
                        }
                        catch (Java.Lang.Exception e)
                        {
                            argsClass = new Class[0];
                            argsValue = new Java.Lang.Object[0];
                        }
                    }

                    try
                    {
                        view.Class.GetMethod(functionName, argsClass).Invoke(view, argsValue);
                    }
                    catch (SecurityException e)
                    {
                    }
                    catch (NoSuchMethodException e)
                    {
                        e.PrintStackTrace();
                    }

                }
                catch (Java.Lang.Exception e)
                {
                    e.PrintStackTrace();
                }
            }

        }

        /**
     * return the id (from the R.java autogenerated class) of the drawable that pass its name as argument
     */
        public static int getDrawableId(Context context, string name)
        {
            return context.Resources.GetIdentifier(name, "drawable", context.PackageName);
        }

        /**
         * return the id (from the R.java autogenerated class) of the string that pass its name as argument
         */
        public static int getStringId(Context context, string name)
        {
            return context.Resources.GetIdentifier(name, "string", context.PackageName);
        }

        /**
         * convert densityPixel to pixel
         */
        public static float dpToPx(float dp)
        {
            return TypedValue.ApplyDimension( ComplexUnitType.Dip, dp, Resources.System.DisplayMetrics);
            //        return (int) (dp * Resources.getSystem().getDisplayMetrics().density);
        }

        /**
         * convert scalePixel to pixel
         */
        public static float spToPx(float sp)
        {
            return TypedValue.ApplyDimension( ComplexUnitType.Sp, sp, Resources.System.DisplayMetrics);
        }

        /**
         * convert pixel to densityPixel
         */
        public static float pxToDp(int px)
        {
            return (px / Resources.System.DisplayMetrics.Density);
        }

        /**
         * convert pixel to scaledDensityPixel
         */
        public static float pxToSp(int px)
        {
            return (px / Resources.System.DisplayMetrics.ScaledDensity);
            //        return (int) TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_SP, px, Resources.getSystem().getDisplayMetrics());
        }

        /**
         * convert densityPixel to scaledDensityPixel
         */
        public static float dpToSp(float dp)
        {
            return (int)(dpToPx(dp) / Resources.System.DisplayMetrics.ScaledDensity);
        }

        /**
         * return device Width
         */
        public static int deviceWidth()
        {
            return Resources.System.DisplayMetrics.WidthPixels;
        }

        /**
     * get ViewHolder class and make reference for evert @link(DynamicViewId) to the actual view
     * if target contains HashMap<String, Integer> will replaced with the idsMap
     */
        public static void parseDynamicView(Java.Lang.Object target, View container, JavaDictionary<string,int> idsMap)
        {
            foreach (System.Reflection.PropertyInfo propertyInfo in target.GetType().GetProperties())
            {
                var customAttributeData = from data in propertyInfo.GetCustomAttributes(true) where data.GetType() == typeof(DynamicViewId) select data;

                if (customAttributeData.Count() > 0)
                {
                    /* if variable is annotated with @DynamicViewId */
                    DynamicViewId dynamicViewIdAnnotation = (DynamicViewId)customAttributeData.FirstOrDefault();
                    /* get the Id of the view. if it is not set in annotation user the variable name */
                    string id = dynamicViewIdAnnotation.Id;
                    if (id.Equals(""))
                        id = propertyInfo.Name;// field.Name;//.getName();
                    if (idsMap.ContainsKey(id))
                    {
                        try
                        {
                            /* get the view Id from the Hashmap and make the connection to the real View */
                            propertyInfo.SetValue(target, container.FindViewById(idsMap[id]));
                            //field.Set(target, container.FindViewById(idsMap[id]));
                        }
                        catch (IllegalArgumentException e)
                        {
                        }
                        catch (IllegalAccessException e)
                        {
                            e.PrintStackTrace();
                        }
                    }
                }
                else if ((propertyInfo.Name.Equals("ids") && (propertyInfo.GetType() == idsMap.GetType())))
                {
                    try
                    {
                        propertyInfo.SetValue(target, idsMap);
                        //field.Set(target, idsMap);
                    }
                    catch (IllegalArgumentException e)
                    {
                    }
                    catch (IllegalAccessException e)
                    {
                        e.PrintStackTrace();
                    }
                }
            }
            //foreach (Java.Lang.Reflect.Field field in target.Class.GetDeclaredFields())
            //{
            //    Java.Lang.Annotation.IAnnotation[] annotation = field.GetAnnotations();
            //    if (field.IsAnnotationPresent(Java.Lang.Class.FromType(typeof(DynamicViewId))))
            //    {
            //        /* if variable is annotated with @DynamicViewId */
            //        DynamicViewId dynamicViewIdAnnotation = (DynamicViewId)field.GetAnnotation(Java.Lang.Class.FromType(typeof(DynamicViewId)));
            //        /* get the Id of the view. if it is not set in annotation user the variable name */
            //        string id = dynamicViewIdAnnotation.id;
            //        if (id.Equals(""))
            //            id = field.Name;//.getName();
            //        if (idsMap.ContainsKey(id))
            //        {
            //            try
            //            {
            //                /* get the view Id from the Hashmap and make the connection to the real View */
            //                field.Set(target, container.FindViewById(idsMap[id]));
            //            }
            //            catch (IllegalArgumentException e)
            //            {
            //            }
            //            catch (IllegalAccessException e)
            //            {
            //                e.PrintStackTrace();
            //            }
            //        }
            //    }
            //    else if ((field.Name.Equals("ids") && (field.GetType() == idsMap.GetType())))
            //    {
            //        try
            //        {
            //            field.Set(target, idsMap);
            //        }
            //        catch (IllegalArgumentException e)
            //        {
            //        }
            //        catch (IllegalAccessException e)
            //        {
            //            e.PrintStackTrace();
            //        }
            //    }
            //}

        }


        /**
     * get ViewHolder class and make reference for evert @link(DynamicViewId) to the actual view
     * if target contains HashMap<String, Integer> will replaced with the idsMap
     */
        public static void parseDynamicView(object target, View container, JavaDictionary<string, int> idsMap)
        {
            System.Reflection.FieldInfo[] fieldInfos = target.GetType().GetFields();
            foreach (System.Reflection.FieldInfo fi in fieldInfos)
            {
                var customAttributeData = from data in fi.CustomAttributes where data.AttributeType == typeof(DynamicViewId) select data;
                //System.Reflection.CustomAttributeData customAttributeData = fi.CustomAttributes.First();
                //DynamicViewId id = (DynamicViewId)fi.GetCustomAttributes(typeof(DynamicViewId), false).FirstOrDefault();
                //if (fi.)
            }

            //foreach (Java.Lang.Reflect.Field field in target.Class.GetDeclaredFields())
            //{
            //    if (field.IsAnnotationPresent(Java.Lang.Class.FromType(typeof(DynamicViewId))))
            //    {
            //        /* if variable is annotated with @DynamicViewId */
            //        DynamicViewId dynamicViewIdAnnotation = (DynamicViewId)field.GetAnnotation(Java.Lang.Class.FromType(typeof(DynamicViewId)));
            //        /* get the Id of the view. if it is not set in annotation user the variable name */
            //        string id = dynamicViewIdAnnotation.id;
            //        if (id.Equals(""))
            //            id = field.Name;//.getName();
            //        if (idsMap.ContainsKey(id))
            //        {
            //            try
            //            {
            //                /* get the view Id from the Hashmap and make the connection to the real View */
            //                field.Set(target, container.FindViewById(idsMap[id]));
            //            }
            //            catch (IllegalArgumentException e)
            //            {
            //            }
            //            catch (IllegalAccessException e)
            //            {
            //                e.PrintStackTrace();
            //            }
            //        }
            //    }
            //    else if ((field.Name.Equals("ids") && (field.GetType() == idsMap.GetType())))
            //    {
            //        try
            //        {
            //            field.Set(target, idsMap);
            //        }
            //        catch (IllegalArgumentException e)
            //        {
            //        }
            //        catch (IllegalAccessException e)
            //        {
            //            e.PrintStackTrace();
            //        }
            //    }
            //}

        }

        private static Java.Lang.Object getFromJSON(JSONObject json, string name, Class clazz)
        {
            if ((clazz == Java.Lang.Class.FromType(typeof(Integer))))
            {
                return json.GetInt(name);
            }
            else if ((clazz == Java.Lang.Class.FromType(typeof(Java.Lang.Boolean))))
            {
                return json.GetBoolean(name);
            }
            else if ((clazz == Java.Lang.Class.FromType(typeof(Java.Lang.Double))))
            {
                return json.GetDouble(name);
            }
            else if ((clazz == Java.Lang.Class.FromType(typeof(Float))))
            {
                return (float) json.GetDouble(name);
            }
            else if ((clazz == Java.Lang.Class.FromType(typeof(Long))))
            {
                return json.GetLong(name);
            }
            else if (clazz == Java.Lang.Class.FromType(typeof(Java.Lang.String)))
            {
                return json.GetString(name);
            }
            else if (clazz == Java.Lang.Class.FromType(typeof(JSONObject)))
            {
                return json.GetJSONObject(name);
            }
            else
            {
                return json.Get(name);

            }
        }

        public static bool classExists(string className)
        {
            try
            {
                Class.ForName(className);
                return true;
            }
            catch (ClassNotFoundException ex)
            {
                return false;
            }
        }


    }
}