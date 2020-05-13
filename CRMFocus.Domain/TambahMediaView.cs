using System.Collections.Generic;

namespace CRMFocus.Domain
{
    public class TambahMediaView
    {
        public string SMSScript { get; set; }
        public List<CallScriptView> CallScripts { get; set;}
    }

    public class CallScriptView
    {
        public byte TipePertanyaan { get; set; }
        public string Pertanyaan { get; set; }
        public int ScriptCode { get; set; }
        public List<ScriptAnswerView> Jawabans { get; set; }
    }

    public class ScriptAnswerView
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }

    public class AnswerOfScriptView
    {
        public List<CallScriptView> Qas { get; set; }
        public int CustomerCode { get; set; }
    }

}
