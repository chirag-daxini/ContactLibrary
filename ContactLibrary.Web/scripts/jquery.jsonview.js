
/*!
jQuery JSONView.
Licensed under the MIT License.
 */
(function (jQuery) {
    var $, Collapser, jsonFormatter, jsonView;
    jsonFormatter = (function () {
        function jsonFormatter(options) {
            if (options == null) {
                options = {};
            }
            this.options = options;
        }

        jsonFormatter.prototype.htmlEncode = function (html) {
            if (html !== null) {
                return html.toString().replace(/&/g, "&amp;").replace(/"/g, "&quot;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
            } else {
                return '';
            }
        };

        jsonFormatter.prototype.jsString = function (s) {
            s = JSON.stringify(s).slice(1, -1);
            return this.htmlEncode(s);
        };

        jsonFormatter.prototype.decorateWithSpan = function (value, className) {
            return "<span class=\"" + className + "\">" + (this.htmlEncode(value)) + "</span>";
        };

        jsonFormatter.prototype.valueToHTML = function (value, level) {
            var valueType;
            if (level == null) {
                level = 0;
            }
            valueType = Object.prototype.toString.call(value).match(/\s(.+)]/)[1].toLowerCase();
            return this["" + valueType + "ToHTML"].call(this, value, level);
        };

        jsonFormatter.prototype.nullToHTML = function (value) {
            return this.decorateWithSpan('null', 'null');
        };

        jsonFormatter.prototype.numberToHTML = function (value) {
            return this.decorateWithSpan(value, 'num');
        };

        jsonFormatter.prototype.stringToHTML = function (value) {
            var multilineClass, newLinePattern;
            if (/^(http|https|file):\/\/[^\s]+$/i.test(value)) {
                return "<a href=\"" + (this.htmlEncode(value)) + "\"><span class=\"q\">\"</span>" + (this.jsString(value)) + "<span class=\"q\">\"</span></a>";
            } else {
                multilineClass = '';
                value = this.jsString(value);
                if (this.options.nl2br) {
                    newLinePattern = /([^>\\r\\n]?)(\\r\\n|\\n\\r|\\r|\\n)/g;
                    if (newLinePattern.test(value)) {
                        multilineClass = ' multiline';
                        value = (value + '').replace(newLinePattern, '$1' + '<br />');
                    }
                }
                return "<span class=\"string" + multilineClass + "\">\"" + value + "\"</span>";
            }
        };

        jsonFormatter.prototype.booleanToHTML = function (value) {
            return this.decorateWithSpan(value, 'bool');
        };

        jsonFormatter.prototype.arrayToHTML = function (array, level) {
            var collapsible, hasContents, index, numProps, output, value, i, len;
            if (level == null) {
                level = 0;
            }
            hasContents = false;
            output = '';
            numProps = array.length;
            for (index = i = 0, len = array.length; i < len; index = ++i) {
                value = array[index];
                hasContents = true;
                output += '<li>' + this.valueToHTML(value, level + 1);
                if (numProps > 1) {
                    output += ',';
                }
                output += '</li>';
                numProps--;
            }
            if (hasContents) {
                collapsible = level === 0 ? '' : ' collapsible';
                return "[<ul class=\"array level" + level + collapsible + "\">" + output + "</ul>]";
            } else {
                return '[ ]';
            }
        };

        jsonFormatter.prototype.objectToHTML = function (object, level) {
            var collapsible, hasContents, key, numProps, output, prop, value;
            if (level == null) {
                level = 0;
            }
            hasContents = false;
            output = '';
            numProps = 0;
            for (prop in object) {
                numProps++;
            }
            for (prop in object) {
                value = object[prop];
                hasContents = true;
                key = this.options.escape ? this.jsString(prop) : prop;
                output += "<li><span class=\"prop\"><span class=\"q\">\"</span>" + key + "<span class=\"q\">\"</span></span>: " + (this.valueToHTML(value, level + 1));
                if (numProps > 1) {
                    output += ',';
                }
                output += '</li>';
                numProps--;
            }
            if (hasContents) {
                collapsible = level === 0 ? '' : ' collapsible';
                return "{<ul class=\"obj level" + level + collapsible + "\">" + output + "</ul>}";
            } else {
                return '{ }';
            }
        };

        jsonFormatter.prototype.jsonToHTML = function (json) {
            return "<div class=\"jsonview\">" + (this.valueToHTML(json)) + "</div>";
        };

        return jsonFormatter;

    })();
    (typeof module !== "undefined" && module !== null) && (module.exports = jsonFormatter);
    Collapser = (function () {
        function collapser() { }

        collapser.bindEvent = function (item, options) {
            var collapser;
            collapser = document.createElement('div');
            collapser.className = 'collapser';
            collapser.innerHTML = options.collapsed ? '+' : '-';
            collapser.addEventListener('click', (function (_this) {
                return function (event) {
                    return _this.toggle(event.target, options);
                };
            })(this));
            item.insertBefore(collapser, item.firstChild);
            if (options.collapsed) {
                return this.collapse(collapser);
            }
        };

        collapser.expand = function (collapser) {
            var ellipsis, target;
            target = this.collapseTarget(collapser);
            if (target.style.display === '') {
                return;
            }
            ellipsis = target.parentNode.getElementsByClassName('ellipsis')[0];
            target.parentNode.removeChild(ellipsis);
            target.style.display = '';
            return collapser.innerHTML = '-';
        };

        collapser.collapse = function (collapser) {
            var ellipsis, target;
            target = this.collapseTarget(collapser);
            if (target.style.display === 'none') {
                return;
            }
            target.style.display = 'none';
            ellipsis = document.createElement('span');
            ellipsis.className = 'ellipsis';
            ellipsis.innerHTML = ' &hellip; ';
            target.parentNode.insertBefore(ellipsis, target);
            return collapser.innerHTML = '+';
        };

        collapser.toggle = function (collapser, options) {
            var action, collapsers, target, i, len, results;
            if (options == null) {
                options = {};
            }
            target = this.collapseTarget(collapser);
            action = target.style.display === 'none' ? 'expand' : 'collapse';
            if (options.recursive_collapser) {
                collapsers = collapser.parentNode.getElementsByClassName('collapser');
                results = [];
                for (i = 0, len = collapsers.length; i < len; i++) {
                    collapser = collapsers[i];
                    results.push(this[action](collapser));
                }
                return results;
            } else {
                return this[action](collapser);
            }
        };

        collapser.collapseTarget = function (collapser) {
            var target, targets;
            targets = collapser.parentNode.getElementsByClassName('collapsible');
            if (!targets.length) {
                return;
            }
            return target = targets[0];
        };

        return collapser;

    })();
    $ = jQuery;
    jsonView = {
        collapse: function (el) {
            if (el.innerHTML === '-') {
                return Collapser.collapse(el);
            }
        },
        expand: function (el) {
            if (el.innerHTML === '+') {
                return Collapser.expand(el);
            }
        },
        toggle: function (el) {
            return Collapser.toggle(el);
        }
    };
    return $.fn.JSONView = function () {
        var args, defaultOptions, formatter, json, method, options, outputDoc;
        args = arguments;
        if (jsonView[args[0]] != null) {
            method = args[0];
            return this.each(function () {
                var $this, level;
                $this = $(this);
                if (args[1] != null) {
                    level = args[1];
                    return $this.find(".jsonview .collapsible.level" + level).siblings('.collapser').each(function () {
                        return jsonView[method](this);
                    });
                } else {
                    return $this.find('.jsonview > ul > li .collapsible').siblings('.collapser').each(function () {
                        return jsonView[method](this);
                    });
                }
            });
        } else {
            json = args[0];
            options = args[1] || {};
            defaultOptions = {
                collapsed: false,
                nl2br: false,
                recursive_collapser: false,
                escape: true
            };
            options = $.extend(defaultOptions, options);
            formatter = new jsonFormatter({
                nl2br: options.nl2br,
                escape: options.escape
            });
            if (Object.prototype.toString.call(json) === '[object String]') {
                json = JSON.parse(json);
            }
            outputDoc = formatter.jsonToHTML(json);
            return this.each(function () {
                var $this, item, items, i, len, results;
                $this = $(this);
                $this.html(outputDoc);
                items = $this[0].getElementsByClassName('collapsible');
                results = [];
                for (i = 0, len = items.length; i < len; i++) {
                    item = items[i];
                    if (item.parentNode.nodeName === 'LI') {
                        results.push(Collapser.bindEvent(item.parentNode, options));
                    } else {
                        results.push(void 0);
                    }
                }
                return results;
            });
        }
    };
})(jQuery);
