<?php
/*
Plugin Name: Gravity Forms LiqPay
Plugin URI: https://www.intelvision.co/
Description: Integrates Gravity Forms with LiqPay Payments Standard, enabling end users to purchase goods and services through Gravity Forms.
Version: 3.3
Author: Intelvision Wordpress Dev team
Author URI: https://www.intelvision.co/
License: GPL-2.0+
Text Domain: gravityformsliqpay
Domain Path: /languages
*/

defined( 'ABSPATH' ) || die();

define( 'GF_LIQPAY_VERSION', '3.3' );

add_action( 'gform_loaded', array( 'GF_LiqPay_Bootstrap', 'load' ), 5 );


add_filter( 'gform_currencies', 'add_inr_currency' );
function add_inr_currency( $currencies ) {
    $currencies['UAH'] = array(
        'name'               => __( 'Украинская гривна', 'gravityforms' ),
        'symbol_left'        => '₴',
        'symbol_right'       => '',
        'symbol_padding'     => ' ',
        'thousand_separator' => ',',
        'decimal_separator'  => '.',
        'decimals'           => 2
    );

    return $currencies;
}

class GF_LiqPay_Bootstrap {

    public static function load() {

        if ( ! method_exists( 'GFForms', 'include_payment_addon_framework' ) ) {
            return;
        }

        require_once( 'gf_liqpay_gw.php' );

        GFAddOn::register( 'GFLiqPay' );
    }
}

function gf_liqpay() {
    return GFLiqPay::get_instance();
}
