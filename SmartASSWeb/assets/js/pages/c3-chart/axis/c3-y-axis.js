$(function() {
    var o = c3.generate({
        bindto: "#y-axis",
        size: { height: 400 },
        color: { pattern: ["#1e88e5", "#E91E63"] },
        data: {
            columns: [
                ["Revenue", 2500, 150, 1000, 100, 500, 30]
            ]
        },
        axis: { y: { tick: { format: d3.format("$,") } } },
        grid: { y: { show: !0 } }
    });
});