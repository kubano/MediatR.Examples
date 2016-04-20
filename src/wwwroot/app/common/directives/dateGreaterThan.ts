/// <reference path="../../typings/_all.d.ts" />
namespace Antares.Common.Directive {
    class DateGreaterThan {
        require: string = 'ngModel';

        constructor(private uibDateParser: any, private validDateRange: Antares.Core.Service.ValidDateRange) {
        }

        link = (scope: ng.IScope, element: ng.IAugmentedJQuery, attrs: ng.IAttributes, ctrl: ng.IControllerService) => {
            var ngModel: ng.INgModelController = element.controller('ngModel');
            var validateDateRange = (inputValue: string): string => {
                if (!inputValue || !attrs['dateGreaterThan']) {
                    ngModel.$setValidity('dateGreaterThan', true);
                    return inputValue;
                }

                var fromDate: Date = this.uibDateParser.parse(attrs['dateGreaterThan'],'dd-MM-yyyy');
                var toDate: Date = this.uibDateParser.parse(inputValue, 'dd-MM-yyyy');
                var isValid: boolean = this.validDateRange.isValidDateRange(fromDate, toDate);
                ngModel.$setValidity('dateGreaterThan', isValid);
                return inputValue;
            };

            ngModel.$parsers.push(validateDateRange);
            ngModel.$formatters.push(validateDateRange);
            attrs.$observe('dateGreaterThan', () => {
                validateDateRange(ngModel.$viewValue);
            });
        }
        static factory() {
            var directive = (uibDateParser: any, validDateRange: Antares.Core.Service.ValidDateRange): DateGreaterThan => {
                return new DateGreaterThan(uibDateParser, validDateRange);
            };

            directive['$inject'] = ["uibDateParser", "validDateRange"];
            return directive;
        }
    }

    angular.module('app').directive('dateGreaterThan', DateGreaterThan.factory());
}