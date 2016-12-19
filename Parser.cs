using System;
using System.Collections;
using System.Collections.Generic;

namespace SimpleRichText
{
    public class Token
    {
        public enum Type
        {
            None,
            Left,
            Right,
            Str,
        }

        public Type type;
        public string val;
    }

    public class ParseException : Exception
    {
        public ParseException(string message) : base(message)
        {
            
        }
    }

    public class Parser
    {
    public const char MK_START = '<';
    public const char MK_END = '>';
    public const char MK_ESC = '$';

        List<Token> TokenList = new List<Token>();
        string orgStr = "";
        int index = 0;//遍历指针

        public List<Token> Parse(string str)
        {
            orgStr = str;
            parse();
            return TokenList;
        }

        void parse()
        {
            if(orgStr == null || orgStr.Length <= 0)
            {
                TokenList.Add(new Token{ type = Token.Type.Str, val = "" });
                return;
            }

            string str = "";
            while(index < orgStr.Length)
            {
                switch(orgStr[index])
                {
                    case MK_START:
                        checkAndSaveStr(str);
                        str = "";
                        TokenList.Add(new Token{ type = Token.Type.Left, val = MK_START.ToString() });
                        ++index;
                        break;

                    case MK_END:
                        checkAndSaveStr(str);
                        str = "";
                        TokenList.Add(new Token{ type = Token.Type.Right, val = MK_END.ToString() });
                        ++index;
                        break;
                    
                    case MK_ESC:
                        if(index + 1 >= orgStr.Length)//以转义字符结尾
                        {
                            str += MK_ESC;
                            ++index;
                        }
                        else
                        {
                            switch(orgStr[index+1])
                            {
                                case MK_START:
                                    str += MK_START;
                                    index += 2;
                                    break;

                                case MK_END:
                                    str += MK_END;
                                    index += 2;
                                    break;

                                case MK_ESC:
                                    str += MK_ESC;
                                    index += 2;
                                    break;
                                
                                default:
                                    str += MK_ESC;
                                    index += 2;
                                    break;
                            }
                        }
                        break;

                    default:
                        str += orgStr[index];
                        ++index;
                        break;
                }
            }
            checkAndSaveStr(str);
        }

        void checkAndSaveStr(string str)
        {
            if(str != null && !str.Equals(""))
            {
                TokenList.Add(new Token{ type = Token.Type.Str, val = str });
            }
        }
    }
}

