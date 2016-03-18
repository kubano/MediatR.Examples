﻿/// <reference path="../../../typings/_all.d.ts" />

module Antares {
    import RequirementViewController = Requirement.View.RequirementViewController;
    describe('Given view requirement page is loaded', () => {
        var scope: ng.IScope,
            element: ng.IAugmentedJQuery,
            $http: ng.IHttpBackendService,
            filter: ng.IFilterService;

        var controller: RequirementViewController;

        var testError = (code, title) => {
            describe(title, () => {
                beforeEach(angular.mock.module('app'));
                beforeEach(inject((
                    $rootScope: ng.IRootScopeService,
                    $compile: ng.ICompileService,
                    $httpBackend: ng.IHttpBackendService) => {

                    $http = $httpBackend;
                    $http.whenGET(/\/api\/requirement/).respond(() => {
                        return [code, 'error'];
                    });

                    scope = $rootScope.$new();
                    element = $compile('<requirement-view></requirement-view>')(scope);
                    scope.$apply();

                    controller = element.controller('requirementView');

                    scope.$apply();
                }));

                it('the loading message appears', () => {
                    expect(controller.isLoading).toBeTruthy();
                });

                it('the "not found" error messsage appears', () => {
                    $http.flush();
                    scope.$apply();

                    expect(controller.hasError).toBeTruthy();
                    expect(controller.errorStatus).toBe(code);
                    expect(controller.isLoading).toBeFalsy();
                });
            });
        }

        testError(404, 'and id of not existing requirement is provided');
        testError(500, 'and server returns unexpected error');

        describe('and proper requirement id is provided', () => {
            var requirementMock: any = {
                createDate: '2016-03-17T12:35:29.82',
                maxPrice: 444,
                minPrice: 0,
                maxBedrooms: 3,
                maxReceptionRooms: 2,
                maxBathrooms: 2,
                maxParkingSpaces: 12,
                maxArea: 1234,
                maxLandArea: 5678,
                description: 'sample',
                contacts: [
                    { id: '1', firstName: 'John', surname: 'Doe', title: 'Mr.' },
                    { id: '2', firstName: 'Jane', surname: 'Doe', title: 'Mrs.' }
                ]
            };

            beforeEach(angular.mock.module('app'));
            beforeEach(inject((
                $rootScope: ng.IRootScopeService,
                $compile: ng.ICompileService,
                $httpBackend: ng.IHttpBackendService,
                $filter: ng.IFilterService) =>{

                filter = $filter;
                $http = $httpBackend;
                $http.whenGET(/\/api\/requirement/).respond(() => {
                    return [200, requirementMock];
                });

                scope = $rootScope.$new();
                element = $compile('<requirement-view></requirement-view>')(scope);
                $http.flush();
                scope.$apply();

                controller = element.controller('requirementView');
            }));

            it('loading message disappears', () => {
                expect(controller.isLoading).toBeFalsy();
            });

            it('requirement details are loaded', () => {
                for (var property in requirementMock) {
                    if (requirementMock.hasOwnProperty(property)) {
                        var value = requirementMock[property];
                        if (angular.isArray(value)) {
                            expect(controller.requirement[property].length).toBe(value.length);
                        }
                        else {
                            expect(controller.requirement[property]).toBe(value);
                        }
                    }
                }
            });

            it('header information is displayed', () => {
                var header = element.find('.requirement-details-header');
                var createDate = header.find('[translate="REQUIREMENT.VIEW.CREATEDDATE"]').next();
                var formattedDate = filter('date')(requirementMock.createDate, 'longDate');

                expect(createDate.length).toBe(1);
                expect(createDate.text()).toBe(formattedDate);
            });

            xit('location requirements are displayed', () => {
                // TODO: after address view component is implemented
            });

            it('basic property requirements are displayed in proper order and with correct values', () =>{
                var expectedBasicDetailsResults = [
                    { label : 'REQUIREMENT.VIEW.PRICE', min : 'vm.requirement.minPrice', max : 'vm.requirement.maxPrice', suffix : "'GBP'" },
                    { label : 'REQUIREMENT.VIEW.BEDROOMS', min : 'vm.requirement.minBedrooms', max : 'vm.requirement.maxBedrooms', suffix : null },
                    { label : 'REQUIREMENT.VIEW.RECEPTIONROOMS', min : 'vm.requirement.minReceptionRooms', max : 'vm.requirement.maxReceptionRooms', suffix : null },
                    { label : 'REQUIREMENT.VIEW.BATHROOMS', min : 'vm.requirement.minBathrooms', max : 'vm.requirement.maxBathrooms', suffix : null },
                    { label : 'REQUIREMENT.VIEW.PARKINGSPACES', min : 'vm.requirement.minParkingSpaces', max : 'vm.requirement.maxParkingSpaces', suffix : null },
                    { label : 'REQUIREMENT.VIEW.AREA', min : 'vm.requirement.minArea', max : 'vm.requirement.maxArea', suffix : "'sq ft'" },
                    { label : 'REQUIREMENT.VIEW.LANDAREA', min : 'vm.requirement.minLandArea', max : 'vm.requirement.maxLandArea', suffix : "'sq ft'" }
                ];

                var rangeViewElements = element.find('range-view');

                expect(rangeViewElements.length).toBe(expectedBasicDetailsResults.length);
                for (var i = 0; i < rangeViewElements.length; i++) {
                    var rangeView = rangeViewElements[i];
                    var expected = expectedBasicDetailsResults[i];
                    expect(rangeView.getAttribute('label')).toBe(expected.label);
                    expect(rangeView.getAttribute('min')).toBe(expected.min);
                    expect(rangeView.getAttribute('max')).toBe(expected.max);
                    expect(rangeView.getAttribute('suffix')).toBe(expected.suffix);
                };
                
            });

            it('applicant list is displayed', () => {
                var applicantList = element.find('#applicant-list');
                var applicants = applicantList.find('.applicant');

                expect(applicantList.length).toBe(1);
                expect(applicants.length).toBe(requirementMock.contacts.length);
            });
        });

    });
}