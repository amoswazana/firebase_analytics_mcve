using UnityEngine;
using Firebase.Analytics;

public class AnalyticsMaster : MonoBehaviour {

    public void TestButtonPressed() {
        LogEvent("test_event", "test_param_key", "test_param_value");
    }

    public static void LogEvent(string eventName, string parameterName, string parameterValue) {
        LogEvent(eventName, new AllAnalyticsParam[] { new AllAnalyticsParam(parameterName, parameterValue) });
    }

    public static void LogEvent(string eventName, params AllAnalyticsParam[] eventParams) {
#if UNITY_EDITOR
        string log = "!@! Logging Event: " + eventName + ", params: ";
#endif
        if (eventParams.Length > 0) {
            Parameter[] paramList = new Parameter[eventParams.Length];
            for (int i = 0; i < eventParams.Length; i++) {
                switch (eventParams[i].ParamType) {
                    case AllAnalyticsParam.ParamTypes.String:
#if UNITY_EDITOR
                        log += eventParams[i].ParamName + ": " + eventParams[i].ParamValStr + ", ";
#endif
                        paramList[i] = new Parameter(eventParams[i].ParamName, eventParams[i].ParamValStr);
                        break;
                    case AllAnalyticsParam.ParamTypes.Long:
#if UNITY_EDITOR
                        log += eventParams[i].ParamName + ": " + eventParams[i].ParamValLong + ", ";
#endif
                        paramList[i] = new Parameter(eventParams[i].ParamName, eventParams[i].ParamValLong);
                        break;
                    case AllAnalyticsParam.ParamTypes.Double:
#if UNITY_EDITOR
                        log += eventParams[i].ParamName + ": " + eventParams[i].ParamValDbl + ", ";
#endif
                        paramList[i] = new Parameter(eventParams[i].ParamName, eventParams[i].ParamValDbl);
                        break;
                }

            }
#if UNITY_EDITOR
            Debug.Log(log);
#endif
            FirebaseAnalytics.LogEvent(eventName, paramList);

        } else {
#if UNITY_EDITOR
            Debug.Log("!@! Loggin Event: " + eventName);
#endif
            FirebaseAnalytics.LogEvent(eventName);
        }
    }

    public class AllAnalyticsParam {

        public enum ParamTypes { String, Long, Double };

        public string ParamName;
        public string ParamValStr;
        public long ParamValLong;
        public double ParamValDbl;

        public ParamTypes ParamType;

        public AllAnalyticsParam(string parameterName, string parameterValue) {
            ParamType = ParamTypes.String;
            ParamName = parameterName;
            ParamValStr = parameterValue;
        }

        public AllAnalyticsParam(string parameterName, long parameterValue) {
            ParamType = ParamTypes.Long;
            ParamName = parameterName;
            ParamValLong = parameterValue;
        }

        public AllAnalyticsParam(string parameterName, double parameterValue) {
            ParamType = ParamTypes.Double;
            ParamName = parameterName;
            ParamValDbl = parameterValue;
        }
    }
}