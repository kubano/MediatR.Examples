﻿/// <reference path="../../../../typings/_all.d.ts" />

module Antares.Common.Component {
    angular.module('app').component('addressFormEdit', {
        templateUrl: 'app/common/components/addressForm/addressFormEdit/addressFormEdit.html',
        controllerAs: 'atvm',
        controller: 'AddressFormEditController',
        bindings: {
            entityTypeCode: '<',
            address: '='
        }
    });
}