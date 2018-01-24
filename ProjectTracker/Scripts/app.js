$(function () {
    $('#cBeginDate').datetimepicker({
        locale: 'ru',
        stepping: 10,
        format: 'DD.MM.YYYY',
        daysOfWeekDisabled: [0, 6]
    });
    $('#cEndDate').datetimepicker({
        locale: 'ru',
        stepping: 10,
        format: 'DD.MM.YYYY',
        daysOfWeekDisabled: [0, 6]
    });    
});