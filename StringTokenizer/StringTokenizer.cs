/*
Open source MIT License
Copyright (c) 2017 Fred A Ekstrand Jr.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

81M6-PP53W-XUFCE-R6E32-4M105-A1X5A-Z6J6
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ekstrand.Text
{
    /*
        The StringTokenizer is the equivalent to Java version.
    */
    public sealed class StringTokenizer : IEnumerator, IEnumerable
    {
        #region Class Global variables and objects

        private int m_Position;
        private string m_String;
        private int m_Length;
        private string m_Delimiters;
        private bool m_ReturnDelimiters;
        private List<string> m_tokens = new List<string>();
        private static string m_DefaultDelimiters = " \t\n\r\f";

        #endregion

        #region Constructor
        /// <summary>
        /// Base constructor to initialize StringTokenizer
        /// </summary>
        /// <remarks>
        ///  Use StringSource properties to set string to be tokenized"
        /// </remarks>
        public StringTokenizer() : this("", m_DefaultDelimiters, false)
        { }

        /// <summary>
        /// Constructor to initialize StringTokenizer
        /// </summary>
        /// <param name="str">String to be tokenized.</param>
        public StringTokenizer(string str) : this(str, m_DefaultDelimiters, false)
        {
        }

        /// <summary>
        /// Constructor to initialize StringTokenizer
        /// </summary>
        /// <param name="str">String to be tokenized.</param>
        /// <param name="delim">String defined delimiters use to tokenized the given string</param>
        /// <remarks>String delimiters would be evaluate each character as separate delimiters to tokenize a given string.</remarks>
        public StringTokenizer(string str, string delim) : this(str, delim, false)
        {
        }

        /// <summary>
        /// Constructor to initialize String Tokenizer with defined string delimiters and return delimiters
        /// </summary>
        /// <param name="str">String to be tokenized.</param>
        /// <param name="delim">String defined delimiters use to tokenized the given string</param>
        /// <param name="retDelims">Boolean When true would return each delimiter as tokens otherwise the delimiters would not be returned. </param>
        /// <remarks>String delimiters would be evaluate each character as separate delimiters to tokenize a given string.</remarks>
        public StringTokenizer(string str, string delim, bool retDelims)
        {
            m_Length = str.Length;
            this.m_String = str;
            this.m_Delimiters = delim;
            this.m_ReturnDelimiters = retDelims;
            this.m_Position = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Replaces the string to be tokenized and reset tokenizer to the beginning.
        /// </summary>
        public string StringSource
        {
            get { return m_String; }
            set
            {
                if (!m_String.Equals(value))
                {
                    m_String = value;
                    ResetTokenizer();
                }
            }
        }

        /// <summary>
        /// Replaces the current delimiters with the new given delimiters.
        /// </summary>
        /// <remarks>
        /// The next token generated would be based on the current delimiters.
        /// </remarks>
        public string Delimiters
        {
            get { return m_Delimiters; }
            set
            {
                m_Delimiters = value;
            }
        }

        /// <summary>
        /// If true, then the delimiter characters are also returned as tokens.
        /// </summary>
        /// <remarks>Default setting is false. Can be set true only through on of the overloaded constructors.</remarks>
        public bool IsReturnDelimiters
        {
            get
            {
                return m_ReturnDelimiters;
            }
            private set
            {
                m_ReturnDelimiters = value;
            }
        }

        /// <summary>
        /// Tests if there are more tokens available to be returned.
        /// </summary>
        /// <returns>Returns true if and only if there is at least one token in the 
        /// string after the current position; false otherwise.</returns>
        public bool HasMoreTokens
        {
            get
            {
                if (!m_ReturnDelimiters)
                {
                    while (m_Position < m_Length && m_Delimiters.IndexOf(m_String[m_Position]) >= 0)
                    {
                        m_Position++;
                    }
                }
                return m_Position < m_Length;
            }
        }

        /// <summary>
        /// Calculates the number of tokens that would be generated.
        /// </summary>
        /// <returns>Return the number of calculated tokens that would be generated.</returns>
        /// <remarks>
        /// Calculation is based on current position in tokenizing the given string. The current position is not advanced during this operation.
        /// </remarks>
        public int Count
        {
            get
            {
                int count = 0;
                int delimiterCount = 0;
                bool tokenFound = false;
                int tmpPos = m_Position;

                while (tmpPos < m_Length)
                {
                    if (m_Delimiters.IndexOf(m_String[tmpPos++]) >= 0)
                    {
                        if (tokenFound)
                        {
                            count++;
                            tokenFound = false;
                        }
                        delimiterCount++;
                    }
                    else
                    {
                        tokenFound = true;
                        while (tmpPos < m_Length && m_Delimiters.IndexOf(m_String[tmpPos]) < 0)
                        {
                            ++tmpPos;
                        }
                    }
                }

                if (tokenFound)
                {
                    count++;
                }

                return m_ReturnDelimiters ? count + delimiterCount : count;
            }
        }

        /// <summary>
        ///  Gets the current element in the collection (Inherited from IEnumerator.)
        /// </summary>
        public object Current
        { // ugly boxing
            get
            {
                return NextToken();
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the next token from this string tokenizer. 
        /// </summary>
        /// <returns>
        /// the next token from this string tokenizer. 
        /// </returns>
        public string NextToken()
        {
            return NextTokenInternal();
        }


        public String NextToken(String delim)
        {
            this.m_Delimiters = delim;
            return NextToken();
        }

        /// <returns>
        /// Return tokens based on current delimiters.
        /// </returns>
        public string[] TokensToArray()
        {
            return TokensToList().ToArray();
        }

        /// <returns>
        /// Return tokens based on current delimiters.
        /// </returns>
        public List<string> TokensToList()
        {
            int holdPos = this.m_Position;
            List<string> items = new List<string>();
            while (HasMoreTokens)
            {
                items.Add(NextToken());
            }

            m_Position = holdPos;

            return items;
        }

        /// <summary>
        /// Sets the StringTokenizer to its starting position before tokenizing the given string.
        /// </summary>
        public void ResetTokenizer()
        {
            m_Position = 0;
            m_tokens.Clear();
            Delimiters = m_DefaultDelimiters;
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.(Inherited from IEnumerator.)
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            return HasMoreTokens;
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.(Inherited from IEnumerator.)
        /// </summary>
        public void Reset()
        {
            ResetTokenizer();
        }

        /// <returns>
        /// Returns an enumerator that iterates through a collection (Inherited from IEnumerable).
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        #endregion

        #region Private Methods

        private string NextTokenInternal()
        {
            if (m_Length == 0)
            {
                return m_String;
            }

            if (m_Position < m_Length && m_Delimiters.IndexOf(m_String[m_Position]) >= 0)
            {
                if (m_ReturnDelimiters)
                {
                    return m_String.Substring(m_Position++, 1);
                }

                while (++m_Position < m_Length && m_Delimiters.IndexOf(m_String[m_Position]) >= 0) ;
            }

            if (m_Position < m_Length)
            {
                int start = m_Position;
                while (++m_Position < m_Length && m_Delimiters.IndexOf(m_String[m_Position]) < 0) ;

                return m_String.Substring(start, m_Position - start);
            }

            return null;

        }

        #endregion

    }
}

