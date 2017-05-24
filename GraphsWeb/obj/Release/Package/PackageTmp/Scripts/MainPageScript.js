var mainApp = angular.module('mainModule', []);

mainApp.controller('MainPageController',
    function ($scope, $http) {
        
        $scope.lineralVisible = true;
        $scope.paralellVisible = false;

        $scope.buttonName = "Відкрити ярусно-паралельну форму";
        $scope.concurrency = "";

        $scope.concurrencyVisible = false;

        $scope.OpenConcurrency = function () {
            $scope.concurrencyVisible = true;
        }
        $scope.OpenCPFClick = function () {
            if ($scope.lineralVisible == true) {
                $scope.lineralVisible = false;
                $scope.paralellVisible = true;
                $scope.buttonName = "Відкрити лінійну форму";
            }
            else {
                $scope.lineralVisible = true;
                $scope.paralellVisible = false;
                $scope.buttonName = "Відкрити ярусно-паралельну форму";
            }
        }
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

mainApp.controller("parallelFormController",
function ($scope) {

    BuildCPF();

    function BuildCPF() {
        var g = {
            nodes: [],
            edges: []
        };

        var levelsLengh = [];
        levelsLengh.push(0);
        for (var i = 1; i < model.ParallelForm.length; i++) {
            if (i == model.ParallelForm.length - 1) {
                levelsLengh.push(model.ParallelForm[0].length / 2);
                g.nodes.push({
                    id: model.NodesConnections[model.NodesConnections.length - 1].Child.Index.toString(),
                    label: model.NodesConnections[model.NodesConnections.length - 1].Child.Value,
                    x: i * 10,
                    y: model.ParallelForm[0].length * 5,
                    size: 10,
                    color: '#666'
                });
            }
            else {
                levelsLengh.push((model.ParallelForm[0].length - model.ParallelForm[i + 1].length) / 2);
            }
        }

        for (var i = 0; i < model.NodesConnections.length; i++) {
            var level = GetLevel(model.NodesConnections[i].Parent.Index);
            g.nodes.push({
                id: model.NodesConnections[i].Parent.Index.toString(),
                label: model.NodesConnections[i].Parent.Value,
                y: level * 10,
                x: levelsLengh[level] * 10,
                size: 10,
                color: '#666'
            });
            levelsLengh[level]++;
            g.edges.push({
                id: 'e' + i,
                source: model.NodesConnections[i].Parent.Index.toString(),
                target: model.NodesConnections[i].Child.Index.toString(),
                size: 5,
                color: '#ccc'
            });
        }

        // Instantiate sigma:
        s = new sigma({
            graph: g,
            container: 'graph-container'
        });
    }
    function BuildOptimizeCPF() {
        
    }

    function GetLevel(index) {
        var levels = model.ParallelForm;

        for (var i = 0; i < levels.length; i++) {
            for (var j = 0; j < levels[i].length; j++) {
                if (levels[i][j] == index)
                    return i;
            }
        }
        return -1;
    }
});

mainApp.controller("lineralFormController",
function () {

    BuildCPF();
    function BuildCPF() {
        var g = {
            nodes: [],
            edges: []
        };
        var countOfArguments = model.ParallelForm[0].length;
        var countOfLevels = model.ParallelForm.length;

        var levelsLengh = 0;

        g.nodes.push({
            id: model.NodesConnections[model.NodesConnections.length - 1].Child.Index.toString(),
            label: model.NodesConnections[model.NodesConnections.length - 1].Child.Index,
            y: countOfLevels * 10,
            x: countOfArguments * 5,
            size: 10,
            color: '#666'
        });

        for (var i = 0; i < model.NodesConnections.length; i++) {
            var level = GetLevel(model.NodesConnections[i].Parent.Index);

            if (i < countOfArguments) {
                g.nodes.push({
                    id: model.NodesConnections[i].Parent.Index.toString(),
                    label: model.NodesConnections[i].Parent.Value,
                    y: 0,
                    x: levelsLengh * 10,
                    size: 10,
                    color: '#666'
                });
                levelsLengh++;
            }
            else {
                g.nodes.push({
                    id: model.NodesConnections[i].Parent.Index.toString(),
                    label: model.NodesConnections[i].Parent.Value,
                    y: (i - countOfArguments + 1) * 10,
                    x: countOfArguments * 5,
                    size: 10,
                    color: '#666'
                });
            }
            g.edges.push({
                id: 'e' + i,
                source: model.NodesConnections[i].Parent.Index.toString(),
                target: model.NodesConnections[i].Child.Index.toString(),
                size: 5,
                color: '#ccc'
            });
        }

        // Instantiate sigma:
        s = new sigma({
            graph: g,
            container: 'lineral-graph-container'
        });
    }

    function GetLevel(index) {
        var levels = model.ParallelForm;

        for (var i = 0; i < levels.length; i++) {
            for (var j = 0; j < levels[i].length; j++) {
                if (levels[i][j] == index)
                    return i;
            }
        }
        return -1;
    }
});
