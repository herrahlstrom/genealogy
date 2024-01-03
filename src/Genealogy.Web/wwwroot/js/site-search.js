function openSearchModal() {
    $('#searchmodal-holder').show(0);
    $('#searchmodal-blender').fadeIn('fast');

    const blenderZindex = parseInt($('#searchmodal-blender').css('z-index'));
    $('.search-groupbox').css('z-index', blenderZindex + 1);
}

function hideSearchModal() {
    $('#searchmodal-blender').fadeOut('fast');
    $('#searchmodal-holder').hide(0);
    $('.search-groupbox').css('z-index', '');
}
