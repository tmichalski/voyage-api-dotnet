(function() {
'use strict';

    // Usage:
    // 
    // Creates:
    // 


    angular
        .module('lss-launchpad')
        .component('lssSecureNav', {
            templateUrl: '/app/nav/secure-nav.component.html',
            controller: SecureNavController,
            controllerAs: 'vm',
            bindings: {

            },
        });

    SecureNavController.$inject = ['authorizationService', 'lssConstants'];
    function SecureNavController(authorizationService, constants) {
        var vm = this;
        vm.states = [];


        ////////////////


        vm.$onInit = function() { 
            _refreshNavigation();
        };

        vm.$onChanges = function(changesObj) { };
        vm.$onDestory = function() { };

        function _refreshNavigation(){
            vm.states = [];
            _addState(constants.lssClaimType, constants.claims.createClaim, 'Claims', 'addClaim');
            _addState(constants.lssClaimType, constants.claims.assignRole, 'Users', 'userDashboard');
        }

        function _addState(claimType, claimName, stateDisplayName, state){
            if(authorizationService.hasClaim(claimType, claimName)){
                vm.states.push({name: stateDisplayName, state: state});
            }
        }
    }
})();