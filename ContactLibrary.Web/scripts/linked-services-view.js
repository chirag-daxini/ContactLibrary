/**
 * Tree services view.
 * @constructor
 * @param {EventsTree}
 */
function LinkedServicesView(tree) {

    TreeView.call(this, tree);
}


/*
 * Extend parent class.
 */
LinkedServicesView.prototype = Object.create(TreeView.prototype);


/**
 * @override
 */
LinkedServicesView.prototype.nodeClickEventHandler = function (node) {

    if (node.children) {
        node._children = node.children;
        node.children = null;
    } else {
        node.children = node._children;
        node._children = null;
    }

    this._tree._update(node);
};


/**
 * @override
 */
LinkedServicesView.prototype.nodeDoubleClickEventHandler = function (service) {
    
};


/**
 * @override
 */
LinkedServicesView.prototype.getTreeData = function () {

    var tree = {};

    tree.name = this._data.Parent;
    tree.parent = null;
    

    var children = [];

    for (var i = 0; i < this._data.Children.length; i++) {
        var child = this._data.Children[i];
        children.push({
            name: child,
            parent: this._data.Parent
        });
    }

    tree.children = children;

    return [tree];
};