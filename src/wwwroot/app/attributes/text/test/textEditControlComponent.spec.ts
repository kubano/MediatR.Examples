﻿/// <reference path="../../../typings/_all.d.ts" />

module Antares {
    describe('Given text edit control', () => {
        var scope: ng.IScope,
            element: ng.IAugmentedJQuery,
            assertValidator: TestHelpers.AssertValidators;

        var textModelMock: string = '';
        var configMock: any = {
            active: true,
            text: Antares.TestHelpers.ConfigGenerator.generateFieldConfig()
        };
        var schemaMock: Attributes.ITextEditControlSchema = {
            controlId: 'text-id',
            translationKey: 'textTranslationKey',
            fieldName: 'text',
            formName: 'formName'
        };

        var pageObjectSelectors = {
            control: '#' + schemaMock.controlId
        };

        beforeEach(inject((
            $rootScope: ng.IRootScopeService,
            $compile: ng.ICompileService) => {

            scope = $rootScope.$new();
            scope['vm'] = { ngModel: textModelMock, config: configMock, schema: schemaMock };
            element = $compile('<text-edit-control ng-model="vm.ngModel" config="vm.config" schema="vm.schema"></text-edit-control>')(scope);
            scope.$apply();

            assertValidator = new TestHelpers.AssertValidators(element, scope);
        }));

        describe('when config is provided', () => {
            it('then control is displayed', () => {
                var controlElement: ng.IAugmentedJQuery = element.find(pageObjectSelectors.control);
                expect(controlElement.length).toBe(1);
            });

            describe('when control value is required from config', () => {
                beforeEach(() => {
                    configMock.text.required = true;
                    scope.$apply();
                });

                it('and text is empty then validation message is displayed', () => {
                    assertValidator.assertRequiredValidator(null, false, pageObjectSelectors.control);
                });

                it('and text is not empty then validation message is not displayed', () => {
                    assertValidator.assertRequiredValidator('test value', true, pageObjectSelectors.control);
                });
            });

            describe('when control value is not required from config', () => {
                beforeEach(() => {
                    configMock.text.required = false;
                    scope.$apply();
                });

                it('and text is empty then validation message is not displayed', () => {
                    assertValidator.assertRequiredValidator(null, true, pageObjectSelectors.control);
                });
            });
        });

        describe('when config is not provided', () => {
            it('then control is not displayed', () => {
                scope['vm'].config = null;
                scope.$apply();
                var controlElement: ng.IAugmentedJQuery = element.find(pageObjectSelectors.control);
                expect(controlElement.length).toBe(0);
            });
        });
    });
}