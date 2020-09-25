jQuery(document).ready(function () {
	jQuery(".chat_fb").click(function () {
		jQuery('.fchat').toggle('slow');
	});

	/* chat **************/
	$('.side-pnl-msg > .spnl-fb').click(function () {
		$('#cfacebook').css({ 'visibility': 'visible' }).find('a:first')[0].click();
	});
});
