# For.Log
這一個元件建立應用程式級別的 Log記錄器，透過 Owin，所有 request統一使用同一個記錄器

元件在輸出 log 使用多執行緒方式寫出進行實作，每一條輸出都為獨立執行緒，並有實作 thread save

在輸出 log 的過程中不會阻斷主執行緒中流程的執行

# How to use

* 實作輸出的邏輯
  * 建立一個Logger並繼承 BaseLogger
  * 覆寫 6 個 void 如何去輸出你的 Log
  * 當然你可以實作多個Logger分別提供給不同方式的輸出，在後方分別取出使用，例如 LoggerForFile、LoggerForElmah、LoggerForSQL、LoggerForAnyThing
```C#

 public class LoggerService : For.Log.BaseLogger
    {
        public LoggerService(LoggerProperty loggerProperty) : base(loggerProperty)
        {
        }
        
        protected override void WriteFatalAsync(string log, LogInfo logInfo)
        {
            var p = log;
        }

        protected override void WriteErrorAsync(string log, LogInfo logInfo)
        {
            var p = log;
        }

        protected override void WriteWarnAsync(string log, LogInfo logInfo)
        {
            var p = log;
        }

        protected override void WriteInfoAsync(string log, LogInfo logInfo)
        {
            var p = log;
        }

        protected override void WriteDebugAsync(string log, LogInfo logInfo)
        {
            var p = log;
        }

        protected override void WriteTraceAsync(string log, LogInfo logInfo)
        {
            var p = log;
        }
    }
    
```
* 使用owin middleware統一管理 Logger
  * 在 Startup.Auth中建立一個 Logger 實體
  * 將這一個實體放進Middleware中，讓pipeline自動取用
    * 指定Logger要進行動作的Level，未被加入的Level，將在執行階段不會有任何動作(例如在程式中使用 Logger.Warn("test")，在此範例中將不會被輸出)
  * 按照上下文的參考，你可以使用將多個Logger注入Middleware中，例如：app.UseLoggerMiddleware(loggerForFile)...etc
    
```C#
  //Create global logger
  var logger = new LoggerService(new LoggerProperty()
  {
      Level = LogLevel.Debug | LogLevel.Info | LogLevel.Error
  });

  //Logger Middleware
  app.UseLoggerMiddleware(logger);
```

* 開始使用 Logger
  * 取得 Logger 實體
  * 這部份可以取出不同的Logger，例如：LoggerProvider<**LoggerForFile**>.GetLogger()...etc
```C#
  var logger = LoggerProvider<LoggerService>.GetLogger()
```
* 輸出 Log
```C#
  logger.Debug("debug");
  logger.Error("Error");
  logger.Fatal("Fatal");
  logger.Info("Info");
  logger.Warn("Warn");
  logger.Trace("Trace");
```


# History
* V 1.0.0
