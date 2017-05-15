var mainApp = angular.module('mainModule', []);

mainApp.controller('MainPageController',
    function ($scope, $http) {

        $http({ method: 'GET', url: "api/GraphDataApi/GetExampleGraph" }).
            then(function success(response) {

                var nodes = [];
                var edges = [];
                for (var i = 0; i < response.data.length; i++) {
                    nodes.push({
                        id: response.data[i].Parent.Index,
                        label: response.data[i].Parent.Index
                    });
                    edges.push({
                        from: response.data[i].Parent.Index,
                        to: response.data[i].Child.Index
                    });
                }
                nodes.push({
                    id: response.data[response.data.length - 1].Child.Index,
                    label: response.data[response.data.length - 1].Child.Index
                });
                var visnodes = new vis.DataSet(nodes);
                var visedges = new vis.DataSet(edges);

                var container = document.getElementById('mynetwork');
                var data = {
                    nodes: visnodes,
                    edges: visedges
                };
                var options = {
                    layout: {
                        randomSeed: undefined,
                        improvedLayout: true,
                        hierarchical: {
                            enabled: true,
                            levelSeparation: 150,
                            nodeSpacing: 100,
                            treeSpacing: 200,
                            blockShifting: true,
                            edgeMinimization: false,
                            parentCentralization: true,
                            direction: 'DU',        // UD, DU, LR, RL
                            sortMethod: 'directed'  // hubsize, directed
                        }
                    }
                };
                new vis.Network(container, data, options);
            });
    });

mainApp.controller("runExtensionController",
    function ($scope, $http) {
        $scope.fileNameChanged = function (ele) {
            var file = ele.files[0];
            $http({
                    method: 'POST',
                    data: file,
                    url: "/api/GraphsApi/GetCpf"
                })
                .then(function success(response) {
                    alert("YES!");
                });    
        }
    });
