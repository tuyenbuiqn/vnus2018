/**
 * @license Copyright (c) 2003-2018, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function (config) {
    config.toolbarGroups = [
		{ name: "document", groups: ["mode", "document", "doctools"] },
		{ name: "clipboard", groups: ["clipboard", "undo"] },
		{ name: "editing", groups: ["find", "selection", "spellchecker", "editing"] },
		{ name: "forms", groups: ["forms"] },
		"/",
		{ name: "basicstyles", groups: ["basicstyles", "cleanup"] },
		{ name: "paragraph", groups: ["list", "indent", "blocks", "align", "bidi", "paragraph"] },
		{ name: "links", groups: ["links"] },
		{ name: "insert", groups: ["insert"] },
		"/",
		{ name: "styles", groups: ["styles"] },
		{ name: "colors", groups: ["colors"] },
		{ name: "tools", groups: ["tools"] },
		{ name: "others", groups: ["others"] },
		{ name: "about", groups: ["about"] }
    ];

    //config.filebrowserBrowseUrl = '~/plugins/ckfinder/ckfinder.html';
    //config.filebrowserImageBrowseUrl = '~/plugins/ckfinder/ckfinder.html?Type=Images';
    //config.filebrowserFlashBrowseUrl = '~/plugins/ckfinder/ckfinder.html?Type=Flash';
    //config.filebrowserUploadUrl = '~/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    //config.filebrowserImageUploadUrl = '~/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    //config.filebrowserFlashUploadUrl = '~/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
};