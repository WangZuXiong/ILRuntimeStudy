using System;
using System.Collections.Generic;

namespace HotFix_Project
{

    public class MyModel //: BaseModel
    {
        public int id;
    }


    public class InstanceClass
    {
        private int id;

        public InstanceClass()
        {
            UnityEngine.Debug.Log("!!! InstanceClass::InstanceClass()");
            this.id = 0;
        }

        public InstanceClass(int id)
        {
            UnityEngine.Debug.Log("!!! InstanceClass::InstanceClass() id = " + id);
            this.id = id;
        }

        public int ID
        {
            get { return id; }
        }


        public MyModel myModel;

        // static method
        public static void StaticFunTest()
        {
            //if (!HelloWorld.ModelDict.ContainsKey("Test"))
            //    HelloWorld.ModelDict.Add("Test", 1);


            //var t1 = HelloWorld.ModelDict["Test"];

            UnityEngine.Debug.Log(HelloWorld.data++);

            //创建Game Object 
            //使用数据t1

             
           

            //return;

            var t = new List<int>() { 1, 2, 3, 4, 5 };

            foreach (var item in t)
            {
                UnityEngine.Debug.Log(item);

            }
        }

        public static void StaticFunTest2(int a)
        {
            UnityEngine.Debug.Log("!!! InstanceClass.StaticFunTest2(), a=" + a);
        }

        public static void GenericMethod<T>(T a)
        {
            UnityEngine.Debug.Log("!!! InstanceClass.GenericMethod(), a=" + a);
        }

        public void RefOutMethod(int addition, out List<int> lst, ref int val)
        {
            val = val + addition + id;
            lst = new List<int>();
            lst.Add(id);
        }
    }


}
