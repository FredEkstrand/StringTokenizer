using System;
using NUnit.VisualStudio.TestAdapter;
using NUnit.Framework;
using Ekstrand.Text;
using System.Collections.Generic;

/*
 * The tests presented here are just for basic functionality
 */
namespace StringTokenizerTester
{
    [TestFixture]    
    public class StringTokenizerTester
    {
        private const string DefaultDelimiters = " \t\n\r\f";
        private string StringSet_1 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis id.";
        private string StringSet_2 = "In 2005, Halton Borough Council put up a notice to tell the public about its plans to move a path from one place to another. Quite astonishingly, the notice was a 630 word sentence, which picked up one of our Golden Bull awards that year. Here is it in full.";
        private string StringSet_3 = "Lorem*ipsum*dolor*sit*amet,*consectetur*adipiscing*elit.*Duis*id.\n\r\t*publish!@#$%^&*()";

        #region Constructor Tests

        [Test]
        [Category("Constructors")]
        public void NoStringDefaultDelimiters()
        {
            StringTokenizer st = new StringTokenizer();

            // initial state checks from constructor call
            Assert.AreEqual(string.Empty, st.StringSource);
            Assert.AreEqual(DefaultDelimiters, st.Delimiters);
            Assert.AreEqual(false, st.IsReturnDelimiters);
        }

        [Test]
        [Category("Constructors")]
        public void StringWithDefaultDelimiters()
        {
            StringTokenizer st = new StringTokenizer(StringSet_1);

            // initial state checks from constructor call
            Assert.AreEqual(StringSet_1, st.StringSource);
            Assert.AreEqual(DefaultDelimiters, st.Delimiters);
            Assert.AreEqual(false, st.IsReturnDelimiters);
        }

        [Test]
        [Category("Constructors")]
        public void StringDefaultDelimitersReturnDelimiters()
        {
            StringTokenizer st = new StringTokenizer(StringSet_1);

            // initial state checks from constructor call
            Assert.AreEqual(StringSet_1, st.StringSource);
            Assert.AreEqual(DefaultDelimiters, st.Delimiters);
            Assert.AreEqual(false, st.IsReturnDelimiters);
        }

        [Test]
        [Category("Constructors")]
        public void StringCustomDelimitersReturnDelimiters()
        {
            StringTokenizer st = new StringTokenizer(StringSet_3, "*", true);

            // initial state checks from constructor call
            Assert.AreEqual(StringSet_3, st.StringSource);
            Assert.AreEqual("*", st.Delimiters);
            Assert.AreEqual(true, st.IsReturnDelimiters);
        }

        [Test]
        [Category("Constructors")]
        public void StringCustomDelimiters()
        {
            StringTokenizer st = new StringTokenizer(StringSet_3, "*");

            // initial state checks from constructor call
            Assert.AreEqual(StringSet_3, st.StringSource);
            Assert.AreEqual("*", st.Delimiters);
            Assert.AreEqual(false, st.IsReturnDelimiters);
        }

        #endregion

        #region Count Test Sets

        [Test]
        [Category("Count Test Sets")]
        public void CountDefaultDlimiters()
        {
            StringTokenizer st = new StringTokenizer(StringSet_2);

            Assert.AreEqual(50, st.Count);

        }

        [Test]
        [Category("Count Test Sets")]
        public void CountEmptyStringDefaultDlimiters()
        {
            StringTokenizer st = new StringTokenizer("");

            Assert.AreEqual(0, st.Count);
        }

        [Test]
        [Category("Count Test Sets")]
        public void CountCustomDlimiters()
        {
            StringTokenizer st = new StringTokenizer(StringSet_3, "*");

            Assert.AreEqual(12, st.Count);
        }

        #endregion

        #region NextToken Test Sets

        [Test]
        [Category("NextToken Test Sets")]
        public void NextTokenNoTokens()
        {
            StringTokenizer st = new StringTokenizer(StringSet_1);
            string result = string.Empty;

            while (result != null)
            {
                result = st.NextToken();
                Console.WriteLine("Token: {0}", result);
            }

            Assert.AreEqual(null, st.NextToken());
        }

        [Test]
        [Category("NextToken Test Sets")]
        public void NextTokenCount()
        {
            StringTokenizer st = new StringTokenizer(StringSet_2);
            int count = 0;

            while(st.NextToken() != null)
            {
                count++;
            }

            Assert.AreEqual(50, count);
        }

        #endregion

        #region HasMoreTokens Test Sets

        [Test]
        [Category("HasMoreTokens Test Sets")]
        public void HasMoreTokenCount()
        {
            StringTokenizer st = new StringTokenizer(StringSet_1);
            int count = 0;

            while(st.HasMoreTokens)
            {
                st.NextToken();
                count++;
            }

            Assert.AreEqual(10, count);
        }

        #endregion

        #region Delimiters Test Sets

        [Test]
        [Category("Delimiters Test Sets")]
        public void DelimitersSetCheck()
        {
            StringTokenizer st = new StringTokenizer(StringSet_1);

            Assert.AreEqual(DefaultDelimiters, st.Delimiters);
        }

        [Test]
        [Category("Delimiters Test Sets")]
        public void DelimitersCustomSetCheck()
        {
            StringTokenizer st = new StringTokenizer(StringSet_3);
            // state check
            Assert.AreEqual(DefaultDelimiters, st.Delimiters);
            st.Delimiters = "*";
            Assert.AreEqual("*", st.Delimiters); // change check
        }

        [Test]
        [Category("Delimiters Test Sets")]
        public void DelimitersCustomTokenizingChangeCheck()
        {
            StringTokenizer st = new StringTokenizer(StringSet_3);
            // state check
            Assert.AreEqual(DefaultDelimiters, st.Delimiters);

            Console.WriteLine("Token: {0}",st.NextToken());

            st.Delimiters = "*";
            Assert.AreEqual("*", st.Delimiters); // change check

            Console.WriteLine("Count: {0}", st.Count);
            // result check
            Assert.AreEqual(3, st.Count);
        }

        #endregion

        #region Reset Test Sets

        [Test]
        [Category("Reset Test Sets")]
        public void RestBeginningTest()
        {
            StringTokenizer st = new StringTokenizer(StringSet_1);
  
            st.Reset();
            Assert.AreEqual(10, st.Count);

        }

        [Test]
        [Category("Reset Test Sets")]
        public void ResetRandomTest()
        {
            StringTokenizer st = new StringTokenizer(StringSet_1);
            int count = 0;
            bool first = false;
            while(st.HasMoreTokens)
            {
                Console.WriteLine("Token: {0}", st.NextToken());
                count++;
                if(count == 5 && first == false)
                {
                    st.Reset();
                }
            }

            Console.WriteLine("Count: {0}", count);
            Assert.AreEqual(15, count);
        }

        [Test]
        [Category("Reset Test Sets")]
        public void ResetEndedTest()
        {
            StringTokenizer st = new StringTokenizer(StringSet_1);

            while(st.HasMoreTokens)
            {
                st.NextToken();
            }

            st.Reset();
            Assert.AreEqual(10, st.Count);
        }
        #endregion

        #region Token to List Test Sets

        [Test]
        [Category("Token to List Test Sets")]
        public void TokensToListCountTest()
        {
            StringTokenizer st = new StringTokenizer(StringSet_1);
            List<string> items = st.TokensToList();

            Assert.AreEqual(st.Count, items.Count);
        }

        [Test]
        [Category("Token to List Test Sets")]
        public void TokensToListInternalPositionTest()
        {
            StringTokenizer st = new StringTokenizer(StringSet_1);
            st.NextToken();
            st.NextToken();
            List<string> items = st.TokensToList();

            string result = st.NextToken();
            Assert.AreEqual("dolor", result);
        }

        #endregion

        #region Tokens To String Array Test Sets

        [Test]
        [Category("Tokens To String Array Test Sets")]
        public void TokensToStringArrayCount()
        {
            StringTokenizer st = new StringTokenizer(StringSet_1);
            Assert.AreEqual(10, st.TokensToArray().Length);
        }

        #endregion

        #region Enumeration Test Sets

        [Test]
        [Category("Enumeration Test Sets")]
        public void EnumerationTokensCount()
        {
            StringTokenizer st = new StringTokenizer(StringSet_1);
            int count = 0;
            foreach(string s in st)
            {
                Console.WriteLine("Tokens: {0}", s);
                count++;
            }

            Assert.AreEqual(10, count);
        }
        #endregion

    }
}
