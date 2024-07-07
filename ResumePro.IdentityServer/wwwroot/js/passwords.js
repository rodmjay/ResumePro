/// <reference path="../lib/jquery/dist/jquery.js" />
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function() {

    var $checkPassword = $(".check-password");

    $("#eye-on").show();
    $("#eye-off").hide();


    $("#eye-off").click(function() {

        $("#Input_NewPassword").attr("type", "password");
        $("#Input_OldPassword").attr("type", "password");
        $("#Input_Password").attr("type", "password");
        $("#Input_ConfirmPassword").attr("type", "password");

        $("#eye-on").show();
        $("#eye-off").hide();
    });

    $("#eye-on").click(function() {

        $("#Input_NewPassword").attr("type", "text");
        $("#Input_OldPassword").attr("type", "text");
        $("#Input_Password").attr("type", "text");
        $("#Input_ConfirmPassword").attr("type", "text");

        $("#eye-off").show();
        $("#eye-on").hide();
    });

    function applyPasswordCheck() {

        const txt = $checkPassword.val();

        const containsLowercase = $("#contains-lowercase");
        const containsUppercase = $("#contains-uppercase");
        const containsNonAlphaNumeric = $("#contains-nonalphanum");
        const containsNumber = $("#contains-number");
        const containsMinLength = $("#contains-min-length");

        const requirementMet = "requirement-met";

        if (/[a-z]/.test(txt)) {

            containsLowercase
                .addClass(requirementMet);
        } else {
            containsLowercase
                .removeClass(requirementMet);
        }

        if (/[^a-zA-Z\d]/.test(txt)) {

            containsNonAlphaNumeric
                .addClass(requirementMet);
        } else {
            containsNonAlphaNumeric
                .removeClass(requirementMet);
        }

        if (/[A-Z]/.test(txt)) {

            containsUppercase
                .addClass(requirementMet);
        } else {
            containsUppercase
                .removeClass(requirementMet);
        }

        if (/[0-9]/.test(txt)) {

            containsNumber
                .addClass(requirementMet);
        } else {
            containsNumber
                .removeClass(requirementMet);
        }

        if (txt.length >= 8) {

            containsMinLength
                .addClass(requirementMet);
        } else {
            containsMinLength
                .removeClass(requirementMet);
        }
    }

    if ($checkPassword.length) {
        applyPasswordCheck();
        $checkPassword.keyup(function() {

            applyPasswordCheck();
        });
    }
});