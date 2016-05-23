﻿/// <reference path="typings/_all.d.ts" />

module Antares {
    var app: ng.IModule = angular.module('app');

    app.config(['$translateProvider', initTranslations]);
    app.config(['$provide', extendOrderByWithEmptyFields]);
    app.config(['$provide', decorateInputNumber]);
    app.config(['growlProvider', configureGrowl]);

    app.run(['enumService', initEnumService]);

    function initTranslations($translateProvider: any) {
        $translateProvider.useLoader('LocalizationLoaderFactory')
            .registerAvailableLanguageKeys(['en'], {
                'en_*': 'en'
            })
            .preferredLanguage('en')
            .useSanitizeValueStrategy('escape');
    }

    function extendOrderByWithEmptyFields($provider: angular.auto.IProvideService) {
        $provider.decorator('orderByFilter', Common.Decorators.OrderByFilterDecorator.decoratorFunction);
    }

    function decorateInputNumber($provider: angular.auto.IProvideService) {
        $provider.decorator('inputDirective', Common.Decorators.InputNumberDirectiveDecorator.decoratorFunction);
    }

    function initEnumService(enumService: Services.EnumService) {
        enumService.init();
    }

    function configureGrowl(growlProvider: angular.growl.IGrowlProvider){
        growlProvider.globalTimeToLive({ success : 5000 });
        growlProvider.globalPosition('top-center');
        growlProvider.globalDisableCountDown(true);
    }
}