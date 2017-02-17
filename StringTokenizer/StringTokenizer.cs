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

    /// <summary>
    /// StringTokenizer equivalent to the JAVA version.
    /// </summary>
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
        /// Initialize an instance of StringTokenizer
        /// </summary>
        public StringTokenizer() : this("", m_DefaultDelimiters, false)
        { }

        /// <summary>
        /// Initialize an instance of StringTokenizer with a given string using default delimiters.
        /// </summary>
        /// <param name="str">String to be tokenized.</param>
        public StringTokenizer(string str) : this(str, m_DefaultDelimiters, false)
        {
        }

        /// <summary>
        /// Initialize an instance of StringTokenizer with a given string using defined delimiters.
        /// </summary>
        /// <param name="str">String to be tokenized.</param>
        /// <param name="delim">String defined delimiters use to tokenized the given string</param>
        /// <remarks>String delimiters would be evaluate each character as separate delimiters to tokenize a given string.</remarks>
        public StringTokenizer(string str, string delim) : this(str, delim, false)
        {
        }

        /// <summary>
        /// Initialize an instance of StringTokenizer with a given string, defined string delimiters, and return delimiters
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
        /// The string to be tokenized
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
        /// The delimiters to be used to tokenized a string.
        /// </summary>
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
        /// string after the current position otherwise false.</returns>
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
        /// The number of tokens that would be generated.
        /// </summary>
        /// <returns>Return the number of calculated tokens that would be generated.</returns>
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
        /// The next token from the given string. 
        /// </summary>
        public string NextToken()
        {
            return NextTokenInternal();
        }

        /// <summary>
        /// The next token from the given string using the given defined delimiter.
        /// </summary>
        /// <param name="delim">String delimiter to be use to generate this next token and on wards.</param>
        /// <returns></returns>
        public String NextToken(String delim)
        {
            this.m_Delimiters = delim;
            return NextToken();
        }

        /// <summary>
        /// Return an array of tokens based on current set of delimiters.
        /// </summary>
        /// <returns>String array of tokens.</returns>
        public string[] TokensToArray()
        {
            return TokensToList().ToArray();
        }

        /// <summary>
        /// Return a list of tokens based on current set of delimiters.
        /// </summary>
        /// <returns>Return List string tokens.</returns>
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
        /// Reset the StringTokenizer to its starting position and set the delimiters to its default values.
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

        /// <summary>
        ///  Returns an enumerator that iterates through a collection (Inherited from IEnumerable).
        /// </summary>
        /// <returns>Return enumerator.</returns>
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Return the next token based on current delimiters.
        /// </summary>
        /// <returns>Return string token.</returns>
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

