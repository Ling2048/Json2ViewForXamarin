using System;
using Android.Runtime;
using Java.Lang;
using Java.Lang.Annotation;

namespace Json2View
{
    //[AttributeUsage(AttributeTargets.Class)]
    //public class MyRetention : Attribute,IRetention
    //{
    //    public string retentionPolicyName { get; set; }

    //    public IntPtr Handle => throw new NotImplementedException();

    //    public Class AnnotationType()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Dispose()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool Equals(Java.Lang.Object obj)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public RetentionPolicy Value()
    //    {
    //        re
    //        return retentionPolicy;
    //        //throw new NotImplementedException();
    //    }
    //}

    /// <summary>
    /// Annotation to create the class ViewHolder
    /// </summary>

    //[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    //public class DynamicViewId : System.Attribute
    //{
    //    internal string id;

    //    public DynamicViewId(string id = "")
    //    {
    //        this.id = id;
    //    }
    //}


    //[MyRetention(retentionPolicyName = RetentionPolicy.Runtime)]
    //public class DynamicViewId : Java.Lang.Object, IAnnotation//, IRetention, ITarget
    //{
    //    public string id { set; get; }

    //    public Class AnnotationType()
    //    {
    //        return Java.Lang.Class.FromType(typeof(DynamicViewId));
    //        //throw new NotImplementedException();
    //    }
    //}

    [AttributeUsage(AttributeTargets.Property)]
    public class DynamicViewId : Attribute
    {
        public string Id { set; get; }
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(Id)) return "";
        //        return Id;
        //    }
        //    set { Id = value; }
        //}
    }

    //[AttributeUsage(AttributeTargets.Field)]
    //public class DynamicViewId : Attribute
    //{
    //    public string id { set; get; }
    //}
}