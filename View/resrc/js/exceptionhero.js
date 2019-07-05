/// <reference path="../dts/exceptionhero.d.ts" />
var ExceptionHero;
(function (ExceptionHero) {
    var Client = (function () {
        function Client() {
        }
        Client.init = function (apiKey, projectId) {
            window.exceptionhero = new Object();
            window.exceptionhero.apiKey = apiKey;
            window.exceptionhero.projectId = projectId;
            window.exceptionhero.serverUrl = "http://exceptionhero.com/";

            if (Utils.isChrome())
                window.onerror = this.errorHandlerChrome;
else
                window.onerror = this.errorHandler;

            Error.prototype.send = function (priority) {
                Client.sendException(this, priority);
            };

            Error.prototype.addObject = function (identifier, relatedObject) {
                if (!this.relatedObjects)
                    this.relatedObjects = new Array();

                this.relatedObjects.push({ Identifier: identifier, RelatedObject: relatedObject });

                return this;
            };
        };

        Client.setServerUrl = function (serverUrl) {
            window.exceptionhero.serverUrl = serverUrl;
        };

        Client.errorHandlerChrome = function (message, file, line, col, error) {
            var json = Json.parseChromeError(file, line, error);
            Sender.send(json);
        };

        Client.errorHandler = function (message, file, line) {
            var json = Json.parseError(message, file, line);
            Sender.send(json);
        };

        Client.sendException = function (error, priority) {
            if (Utils.isChrome()) {
                var json = Json.parseChromeError(error, priority);
                Sender.send(json);
            } else {
                var json = Json.parseStandardError(error, priority);
                Sender.send(json);
            }
        };
        return Client;
    })();
    ExceptionHero.Client = Client;

    var Json = (function () {
        function Json() {
        }
        Json.parseChromeError = function (error, priority) {
            var json = {
                "language": "javascript",
                "message": error.message,
                "stacktrace": error.stack,
                "projectId": window.exceptionhero.projectId,
                "priority": priority,
                "timestamp": Utils.convertDateTimeToTicks(),
                "environment": {
                    "Useragent": Environment.getBrowser(),
                    "Browser": Environment.getBrowserName(),
                    "Language": Environment.getLanguage(),
                    "Url": Utils.getCurrentPage(),
                    "OS": Environment.getOS(),
                    "Window size": Environment.getWindowSize()
                },
                "hardware": {
                    "Resolution": Environment.getResolution()
                },
                "relatedobjects": RelatedObjects.parseRelatedObjects(error)
            };
            return json;
        };

        Json.parseStandardError = function (error, priority) {
            var json = {
                "language": "javascript",
                "message": error.message,
                "file": error.fileName + ":" + error.lineNumber,
                "stacktrace": error.stack,
                "projectId": window.exceptionhero.projectId,
                "priority": priority,
                "timestamp": Utils.convertDateTimeToTicks(),
                "environment": {
                    "Useragent": Environment.getBrowser(),
                    "Browser": Environment.getBrowserName(),
                    "Language": Environment.getLanguage(),
                    "Url": Utils.getCurrentPage(),
                    "OS": Environment.getOS(),
                    "Window size": Environment.getWindowSize()
                },
                "hardware": {
                    "Resolution": Environment.getResolution()
                },
                "relatedobjects": RelatedObjects.parseRelatedObjects(error)
            };
            return json;
        };

        Json.parseError = function (message, file, line) {
            var json = {
                "language": "javascript",
                "message": message,
                "file": file + ":" + line,
                "projectId": window.exceptionhero.projectId,
                "priority": 3,
                "timestamp": Utils.convertDateTimeToTicks(),
                "environment": {
                    "Useragent": Environment.getBrowser(),
                    "Browser": Environment.getBrowserName(),
                    "Language": Environment.getLanguage(),
                    "Url": Utils.getCurrentPage(),
                    "OS": Environment.getOS(),
                    "Window size": Environment.getWindowSize()
                },
                "hardware": {
                    "Resolution": Environment.getResolution()
                }
            };
            return json;
        };
        return Json;
    })();
    ExceptionHero.Json = Json;

    var Sender = (function () {
        function Sender() {
        }
        Sender.send = function (json) {
            var http = new XMLHttpRequest();
            http.open("POST", window.exceptionhero.serverUrl, true);

            http.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            http.setRequestHeader("apiKey", window.exceptionhero.apiKey);

            http.onreadystatechange = function () {
                if (http.readyState == 4 && http.status == 200) {
                }
            };
            http.send(JSON.stringify(json));
        };
        return Sender;
    })();
    ExceptionHero.Sender = Sender;

    var Utils = (function () {
        function Utils() {
        }
        Utils.isChrome = function () {
            return navigator.userAgent.toLowerCase().indexOf('chrome') > -1;
        };

        Utils.convertDateTimeToTicks = function () {
            var datetime = new Date();
            var offset = datetime.getTimezoneOffset();

            datetime.setHours(datetime.getHours() - (offset / 60));

            return ((datetime.getTime() * 10000) + 621355968000000000);
        };

        Utils.getCurrentPage = function () {
            return window.location.href;
        };
        return Utils;
    })();
    ExceptionHero.Utils = Utils;

    var Environment = (function () {
        function Environment() {
        }
        Environment.getBrowser = function () {
            return navigator.userAgent;
        };

        Environment.getLanguage = function () {
            return navigator.language;
        };

        Environment.getBrowserName = function () {
            return navigator.appName;
        };

        Environment.getOS = function () {
            if (navigator.appVersion.indexOf("Win") != -1)
                return "Windows";
            if (navigator.appVersion.indexOf("Mac") != -1)
                return "MacOS";
            if (navigator.appVersion.indexOf("X11") != -1)
                return "UNIX";
            if (navigator.appVersion.indexOf("Linux") != -1)
                return "Linux";
            return "Unknown";
        };

        Environment.getResolution = function () {
            return screen.width + "x" + screen.height;
        };

        Environment.getWindowSize = function () {
            return window.outerWidth + "x" + window.outerHeight;
        };
        return Environment;
    })();
    ExceptionHero.Environment = Environment;

    var RelatedObjects = (function () {
        function RelatedObjects() {
        }
        RelatedObjects.parseRelatedObjects = function (error) {
            if (!error.relatedObjects)
                return "";

            var o = new Object();

            for (var i = 0; i < error.relatedObjects.length; i++) {
                o[error.relatedObjects[i].Identifier] = error.relatedObjects[i].RelatedObject;
            }

            return o;
        };
        return RelatedObjects;
    })();
    ExceptionHero.RelatedObjects = RelatedObjects;
})(ExceptionHero || (ExceptionHero = {}));
